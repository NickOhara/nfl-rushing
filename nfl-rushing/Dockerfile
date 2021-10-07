#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM node:10-apline as build-node
WORKDIR /ClientApp
COPY CLientApp/package.json .
COPY ClientApp/package-lock.json .
RUN npm install
COPY Clientapp/ .
RUN npm run build

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
ENV BuildingDocker true
WORKDIR /src
COPY ["nfl-rushing.csproj", "nfl-rushing/"]
RUN dotnet restore "nfl-rushing/nfl-rushing.csproj"

WORKDIR "/src/nfl-rushing"
COPY . ./

RUN dotnet build "nfl-rushing.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "nfl-rushing.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "nfl-rushing.dll"]