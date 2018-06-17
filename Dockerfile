FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 9000

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY *.sln ./
COPY src/LocationService.Api/LocationService.Api.csproj src/LocationService.Api/
RUN dotnet restore
COPY . .
WORKDIR /src/src/LocationService.Api/
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "LocationService.Api.dll"]