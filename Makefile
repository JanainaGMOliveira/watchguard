# MIGRATION:=Initial
# ENV:=Debug

# create-network:
# 	docker network create watchguard || exit 0
	
# create-volume:
# 	docker volume create watchguard-warehouse

# remove-volume:
# 	docker volume remove warehouse-watchguard


# # SQL SERVER
# stop-sqlserver:
# 	docker stop sqlserver-watchguard || exit 0
# 	docker rm -f sqlserver-watchguard || exit 0

# build-sqlserver:
# 	docker build \
# 		-f docker/dev/sqlserver.dockerfile \
# 		-t watchguard/sqlserver:warehouse .

# run-sqlserver: stop-sqlserver create-volume
# 	docker run --network watchguard \
# 		--name sqlserver-watchguard -d \
# 		-v sqlserver-volume:/var/opt/mssql \
# 		-p 1433:1433 \
# 		watchguard/sqlserver:warehouse

# stop-watchguard:
# 	docker stop watchguard-warehouse || exit 0
# 	docker rm -f watchguard-warehouse || exit 0

# build-watchguard:
# 	docker build \
# 		-f docker/dev/warehouse.dockerfile \
# 		-t watchguard/warehouse:APIWarehouse .

# run-watchguard: stop-watchguard
# 	docker run --network watchguard \
# 		-e ASPNETCORE_ENVIRONMENT=LocalDocker \
# 		--name watchguard-warehouse -d \
# 		-p 5000:80 \
# 		watchguard/warehouse:APIWarehouse

# run-watchguard-it: stop-watchguard
# 	docker run --network watchguard \
# 		-e ASPNETCORE_ENVIRONMENT=Debug \			
# 		--name watchguard-warehouse -it \
# 		-p 5000:80 \
# 		watchguard/warehouse:APIWarehouse

# # EXTRA
# build: create-network build-sqlserver build-watchguard 
# run: run-sqlserver run-watchguard 
# stop: stop-sqlserver stop-watchguard

# drop-db:
# 	cd APIWarehouse/ && export ASPNETCORE_ENVIRONMENT=LocalDocker && dotnet ef database drop -f

# #LOGS
# show-warehouse: 
# 	docker logs -f watchguard-warehouse

# #MIGRATIONS
# create-migration:
# 	cd APIWarehouse/ && export ASPNETCORE_ENVIRONMENT=Migration && dotnet ef migrations add $(MIGRATION)

# revert-migration:
# 	cd APIWarehouse/ && export ASPNETCORE_ENVIRONMENT=Migration && dotnet ef database update $(MIGRATION)

# remove-migration:
# 	cd APIWarehouse/ && export ASPNETCORE_ENVIRONMENT=Migration && dotnet ef migrations remove -f



ENV:=LocalDocker
MIGRATION:=Initial
IMG_VERSION:=1.0-beta
SQLSERVER_VOLUME_NAME:=sqlserver-volume458

create-network:
	docker network create watchguard || exit 0
	
create-volume:
	docker volume create $(SQLSERVER_VOLUME_NAME)_$(IMG_VERSION)

remove-volume:
	docker volume remove $(SQLSERVER_VOLUME_NAME)_$(IMG_VERSION)


###################
# SQL SERVER
###################

stop-sqlserver:
	docker stop watchguard-sqlserver || exit 0
	docker rm -f watchguard-sqlserver || exit 0

build-sqlserver:
	docker build \
		-f docker/dev/sqlserver.dockerfile \
		-t watchguard/sqlserver:$(IMG_VERSION) .

run-sqlserver: stop-sqlserver create-volume
	docker run --network watchguard \
		--name watchguard-sqlserver -d \
		-v $(SQLSERVER_VOLUME_NAME)_$(IMG_VERSION):/var/opt/mssql \
		-p 1433:1433 \
		watchguard/sqlserver:$(IMG_VERSION)

run-sqlserver-it: stop-sqlserver create-volume
	docker run --network watchguard \
		--name watchguard-sqlserver -it \
		-v $(SQLSERVER_VOLUME_NAME)_$(IMG_VERSION):/var/opt/mssql \
		-p 1433:1433 \
		watchguard/sqlserver:$(IMG_VERSION)

###################
# warehouse
###################

stop-warehouse:
	docker stop watchguard-warehouse || exit 0
	docker rm -f watchguard-warehouse || exit 0

build-warehouse:
	docker build \
		-f docker/dev/warehouse.dockerfile \
		-t watchguard/warehouse:$(IMG_VERSION) .

run-warehouse: stop-warehouse
	docker run --network watchguard \
		-e ASPNETCORE_ENVIRONMENT=$(ENV) \
		--name watchguard-warehouse -d \
		-p 5010:80 \
		watchguard/warehouse:$(IMG_VERSION)

run-warehouse-it: stop-warehouse
	docker run --network watchguard \
		-e ASPNETCORE_ENVIRONMENT=$(ENV) \
		--name watchguard-warehouse -it \
		-p 5010:80 \
		watchguard/warehouse:$(IMG_VERSION)

###################
# EXTRA
###################

build: create-network build-sqlserver build-warehouse
run: run-sqlserver run-warehouse
stop: stop-sqlserver stop-warehouse 
restart: stop build run

drop-db-watchguard:
	cd APIWarehouse/ && export ASPNETCORE_ENVIRONMENT=$(ENV) && dotnet ef database drop -f

prune-all:
	docker system prune -a -f --volumes

force-reset: stop prune-all build run

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

run-migration:
	cd APIWarehouse/ && dotnet ef database update

#LOCAL
run-local: run-sqlserver run-local-warehouse

run-local-warehouse:
	export ASPNETCORE_ENVIRONMENT=Development && \
	gnome-terminal --title="Cadastro" -- bash -c "dotnet run -p APIWarehouse 1>logs/warehouse 2>logs/warehouse.err"


	