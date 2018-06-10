FROM microsoft/aspnetcore-build:2.0 AS build

WORKDIR /src
COPY *.sln ./
COPY src/LocationService.Api/LocationService.Api.csproj src/LocationService.Api/
COPY src/LocationService.Domain/LocationService.Domain.csproj src/LocationService.Domain/
COPY src/LocationService.Infrastructure/LocationService.Infrastructure.csproj src/LocationService.Infrastructure/

RUN dotnet restore
COPY . ./
WORKDIR /src/src/LocationService.Api
RUN dotnet publish -c Release -o out

# Build da imagem
FROM microsoft/aspnetcore:2.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "LocationService.Api.dll"]