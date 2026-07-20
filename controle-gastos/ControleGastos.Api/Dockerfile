# Use a imagem do SDK para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar csproj e restaurar dependências
COPY ["ControleGastos.Api.csproj", "./"]
RUN dotnet restore "ControleGastos.Api.csproj"

# Copiar o resto do código e compilar
COPY . .
RUN dotnet build "ControleGastos.Api.csproj" -c Release -o /app/build
RUN dotnet publish "ControleGastos.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Imagem final de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Criar pasta para o banco de dados (SQLite)
RUN mkdir -p /app/data

# Definir a variável de ambiente para usar a connection string do Docker
ENV ConnectionStrings__DefaultConnection="Data Source=/app/data/gastos.db"

ENTRYPOINT ["dotnet", "ControleGastos.Api.dll"]
