FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /app/

COPY APIWarehouse APIWarehouse
COPY Infra Infra
COPY ModelsAndExtensions ModelsAndExtensions

WORKDIR /app/APIWarehouse/

# Copy everything else and build
# COPY . ./
RUN dotnet publish -c Release -o out APIWarehouse.csproj

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
COPY --from=build-env /app/APIWarehouse/out .

EXPOSE 5010

ENTRYPOINT ["dotnet", "APIWarehouse.dll"]
