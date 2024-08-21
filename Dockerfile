# Etapa 1: Build da aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia os arquivos de projeto e restaura as dependências
COPY *.sln ./
COPY ProductCatalog.Application/*.csproj ./ProductCatalog.Application/
COPY ProductCatalog.Domain/*.csproj ./ProductCatalog.Domain/
COPY ProductCatalog/*.csproj ./ProductCatalog/
COPY ProductCatalog.Infra.Mongo/*.csproj ./ProductCatalog.Infra.Mongo/
COPY ProductCatalog.Infra.Mapper/*.csproj ./ProductCatalog.Infra.Mapper/
COPY ProductCatalog.Infra.ExternalApis/*.csproj ./ProductCatalog.Infra.ExternalApis/
COPY ProductCatalog.Tests/*.csproj ./ProductCatalog.Tests/

RUN dotnet restore --force-evaluate

# Copia o restante dos arquivos do projeto
COPY . .

# Compilar a aplicação
WORKDIR /app/ProductCatalog
RUN dotnet publish -c Release -o /out

# Etapa 2: Criação da imagem de produção
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copiar a aplicação do estágio anterior
COPY --from=build /out .

# Expor a porta usada pela aplicação
EXPOSE 5000

# Comando para iniciar a aplicação
ENTRYPOINT ["dotnet", "ProductCatalog.dll"]
