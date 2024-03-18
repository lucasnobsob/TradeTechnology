# Estágio base para construção da API
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["API/API.csproj", "API/"]
RUN dotnet restore "./API/./API.csproj"
COPY . .
WORKDIR "/src/API"
RUN dotnet build "./API.csproj" -c Release -o /app/build

# Estágio para publicação da API
FROM build AS publish
RUN dotnet publish "./API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Estágio final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Instalar e configurar o MongoDB
FROM ubuntu:latest AS mongodb-install
RUN apt-get update && \
    apt-get install -y mongodb && \
    mkdir -p /data/db
EXPOSE 27017
CMD ["mongod"]
