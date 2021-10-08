FROM node:10-alpine as build-node
WORKDIR /ClientApp
COPY ClientApp/package.json .
COPY ClientApp/package-lock.json .
RUN npm install
COPY ClientApp/ .
RUN npm run build
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-net
ENV BuildingDocker true
WORKDIR /app
COPY *.csproj .
RUN dotnet restore
COPY . .
RUN dotnet build
RUN dotnet publish -o /out
FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /web
COPY --from=build-net /out .
COPY --from=build-node /ClientApp/build ./ClientApp/build
ENTRYPOINT [ "dotnet", "nfl-rushing.dll"]
