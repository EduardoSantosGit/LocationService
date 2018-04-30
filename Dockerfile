FROM microsoft/dotnet:2.0-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 9000

FROM microsoft/dotnet:2.0-sdk AS build
WORKDIR /src
COPY *.sln ./
COPY src/LocationService.Api/LocationService.Api.csproj src/LocationService.Api/
COPY src/LocationService.Domain/LocationService.Domain.csproj src/LocationService.Domain/
COPY src/LocationService.Infrastructure/LocationService.Infrastructure.csproj src/LocationService.Infrastructure/

RUN dotnet restore
COPY . .
WORKDIR /src/src/LocationService.Api
#RUN dotnet build -c Release -o /app

FROM build AS publish
#RUN dotnet publish -c Release -o /app

#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app .
#ENTRYPOINT ["dotnet", "LocationService.dll"]