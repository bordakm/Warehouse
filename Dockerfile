# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY Warehouse/*.csproj ./Warehouse/
RUN dotnet restore

# copy everything else and build app
COPY Warehouse/. ./Warehouse/
WORKDIR /source/Warehouse
RUN dotnet publish -c release -o /app --no-restore
RUN cp -r /source/Warehouse/ClientApp/. /app/ClientApp/


# RUN dotnet tool install --global dotnet-ef
# WORKDIR /source/Warehouse/Warehouse
# RUN dotnet ef migrations add InitialCreate
# RUN dotnet ef database update InitialCreate


# final stage/image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app ./

RUN apt update
RUN apt -y upgrade
RUN apt -y install npm

ENV ASPNETCORE_Environment=Development
ENTRYPOINT ["dotnet", "Warehouse.dll"]
