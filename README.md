# HealthCheckSites
Repositório do artigo escrito sobre .Net Core Worker no blog [Código Maromba](https://codigomaromba.com/2020/07/12/net-tarefas-em-segundo-plano/).

## Executando a aplicação com .Net Sdk
No mesmo diretório onde está o arquivo HealthCheck.csproj execute os seguintes comandos do .Net CLI:

```
dotnet build
dotnet run
```
## Executando a aplicação com Docker
No mesmo diretório onde está o arquivo Dockerfile execute os seguintes comandos no terminal:

```
docker build -t health-check-sites .
docker run --rm -d -p 5000:80 --name health-check-sites health-check-sites
```
