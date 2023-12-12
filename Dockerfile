FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY src/SalesApi/SalesApi.csproj .
RUN dotnet restore
COPY ./src/SalesApi .
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app .

# Define la variable de entorno ASPNETCORE_ENVIRONMENT como development
ENV ASPNETCORE_ENVIRONMENT=development
ENV DOTNET_ENVIRONMENT=development

# Expone el servicio en el puerto 80
EXPOSE 80

ENTRYPOINT ["dotnet", "SalesApi.dll"]
