#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

RUN curl --silent --location https://deb.nodesource.com/setup_10.x | bash -
RUN apt-get install --yes nodejs

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["nfl-rushing.csproj", "nfl-rushing/"]
RUN dotnet restore "nfl-rushing/nfl-rushing.csproj"

WORKDIR "/src/nfl-rushing"
COPY . .

RUN dotnet build "nfl-rushing.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "nfl-rushing.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "nfl-rushing.dll"]