# Use la imagen base del SDK de .NET 8.0 para compilar la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia los archivos del proyecto al contenedor
COPY . .
# Restaura las dependencias y herramientas de NuGet para la solución
RUN dotnet restore

# Publica la aplicación en modo Release en la carpeta 'out'
RUN dotnet publish -c Release -o out

# Use la imagen base de runtime de ASP.NET para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Define el punto de entrada de la aplicación
ENTRYPOINT ["dotnet", "insuranceLeadApi.dll"]
