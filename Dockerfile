# Etapa 1: Build da aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia os arquivos de solução e projetos para restaurar as dependências
COPY *.sln ./
COPY ProductCatalog.Domain/*.csproj ./ProductCatalog.Domain/
COPY ProductCatalog.Application/*.csproj ./ProductCatalog.Application/
COPY ProductCatalog/*.csproj ./ProductCatalog/
COPY ProductCatalog.Infra.Mongo/*.csproj ./ProductCatalog.Infra.Mongo/
COPY ProductCatalog.Infra.Mapper/*.csproj ./ProductCatalog.Infra.Mapper/
COPY ProductCatalog.Infra.ExternalApis/*.csproj ./ProductCatalog.Infra.ExternalApis/
COPY ProductCatalog.Infra.FluentValidation/*.csproj ./ProductCatalog.Infra.FluentValidation/
COPY ProductCatalog.Tests/*.csproj ./ProductCatalog.Tests/

# Restaura as dependências
RUN dotnet restore

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
