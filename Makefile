MIGRATION:=Initial

create-network:
	docker network create watchguard || exit 0
	
create-volume:
	docker volume create watchguard-warehouse

remove-volume:
	docker volume remove warehouse-watchguard


# SQL SERVER
stop-sqlserver:
	docker stop sqlserver-watchguard || exit 0
	docker rm -f sqlserver-watchguard || exit 0

build-sqlserver:
	docker build \
		-f docker/dev/sqlserver.dockerfile \
		-t watchguard/sqlserver:warehouse .

run-sqlserver: stop-sqlserver create-volume
	docker run --network watchguard \
		--name sqlserver-watchguard -d \
		-v sqlserver-volume:/var/opt/mssql \
		-p 1433:1433 \
		watchguard/sqlserver:warehouse

stop-watchguard:
	docker stop watchguard-warehouse || exit 0
	docker rm -f watchguard-warehouse || exit 0

build-watchguard:
	docker build \
		-f docker/dev/warehouse.dockerfile \
		-t watchguard/warehouse:APIWarehouse .

run-watchguard: stop-watchguard
	docker run --network watchguard \
		-e ASPNETCORE_ENVIRONMENT=LocalDocker \
		--name watchguard-warehouse -d \
		-p 5000:80 \
		watchguard/warehouse:APIWarehouse

run-watchguard-it: stop-watchguard
	docker run --network watchguard \
		-e ASPNETCORE_ENVIRONMENT=LocalDocker \			
		--name watchguard-warehouse -it \
		-p 5000:80 \
		watchguard/warehouse:APIWarehouse

# EXTRA
build: create-network build-sqlserver build-watchguard 
run: run-sqlserver run-watchguard 
stop: stop-sqlserver stop-watchguard

drop-db:
	cd APIWarehouse/ && export ASPNETCORE_ENVIRONMENT=LocalDocker && dotnet ef database drop -f

#LOGS
show-warehouse: 
	docker logs -f watchguard-warehouse

#MIGRATIONS
create-migration:
	cd APIWarehouse/ && export ASPNETCORE_ENVIRONMENT=Migration && dotnet ef migrations add $(MIGRATION)

revert-migration:
	cd APIWarehouse/ && export ASPNETCORE_ENVIRONMENT=Migration && dotnet ef database update $(MIGRATION)

remove-migration:
	cd APIWarehouse/ && export ASPNETCORE_ENVIRONMENT=Migration && dotnet ef migrations remove -f
