# GloboTicket App - Microservices Architecture

## System architecture
![System Architecture](system_architecture.png)

## Dependencies
- MSSQL Server
- RabbitMQ
- Elasticsearch

### Execute this script in PowerShell to run all 3 containers:
execute in powershell:
./docker-scripts.ps1


Or you can run them individually as described below.
You can use the scripts in Scripts folder to run docker containers with these dependencies.

### Pull the images:

https://hub.docker.com/_/microsoft-mssql-server
https://hub.docker.com/r/masstransit/rabbitmq
https://www.docker.elastic.co/r/elasticsearch/elasticsearch:7.9.3
```
docker pull mcr.microsoft.com/mssql/server:2019-latest
docker pull masstransit/rabbitmq
docker pull docker.elastic.co/elasticsearch/elasticsearch:7.9.3
```

### Run Elasticsearch container (with volumes to persist data):
```
docker run -d -p 9200:9200 -e "discovery.type=single-node" `
-v esdata:/usr/share/elasticsearch/data `
docker.elastic.co/elasticsearch/elasticsearch:7.9.3
```
You can access it in the browser: http://localhost:9200

You can also get a chrome extension: https://chrome.google.com/webstore/detail/elasticvue/hkedbapjpblbodpgbajblpnlpenaebaa?hl=en

### Run RabbitMQ container:
```
docker run -d -p 15672:15672 -p 5672:5672 masstransit/rabbitmq
```
You can access it in the browser: http://localhost:15672/


### Run MSSQL Server container (with volumes to persist data):
```
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Pass@word1' -p 1433:1433 `
-v sqlvolume:/var/opt/mssql `
-d mcr.microsoft.com/mssql/server:2019-latest
```

## Database connection string (Promotion microservice)
Different environments will require different connection strings.

### Using Docker MSSQL Server container
```json
{
  "ConnectionStrings": {
    "PromotionContext": "Server=localhost,1433;Database=PromotionContext;User Id=sa;Password=Pass@word1;MultipleActiveResultSets=true"
  }
}
```

### Using LocalDB (for development)
```json
{
  "ConnectionStrings": {
    "PromotionContext": "Server=(localdb)\\mssqllocaldb;Database=PromotionContext;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```
You can of course name your database something else.

### Using a local MSSQL Server
If you are using a local MSSQL Server then you need to update your connection string to this:

```json
{
  "ConnectionStrings": {
    "PromotionContext": "Server=localhost\\SQLEXPRESS;Database=PromotionContext;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

## Creating the Database (Promotion microservice)

Install the EF command-line tools in order to work with the application database. NOTE: we need version 5 compatible with .NET 5.
Run this command:

```bash
dotnet tool install --global dotnet-ef --version 5.0.0
```

Initialize the application database by running migrations.
Use the following command in the terminal powershell:

```bash
dotnet ef database update --project GloboTicket.Promotion/
```

or in Visual Studio Package Manager Console:
```powershell
Update-Database -Project GloboTicket.Promotion
```

![Promotion Database](PromotionDatabase.png)

## Running the Promotion Web Application

Start up the Promotion Web application with this command:

```bash
dotnet run --project GloboTicket.Promotion
```

Or run GloboTicket.Promotion from Visual Studio.

## Running the Emailer

The Emailer is a mock service that stands in for a process that emails about new shows.
It requires RabbitMQ to be running because it listens for messages about new shows.

Start the Emailer and schedule a show in the Promotion Web application. You should see the Emailer output in the console verifying that it received the message.

## Running the Indexer

The Indexer listens for messages from the Promotion Web application about changes in the MSSQL database and it updates Elasticsearch database.
Depends on RabbitMQ and Elasticsearch to be running.

## Elasticsearch
Visit [http://localhost:9200](http://localhost:9200) in your browser to verify that it is running.
Then schedule a show in the Promotion Web application and query Elasticsearch at [http://localhost:9200/shows/_search?pretty](http://localhost:9200/shows/_search?pretty).

## Running the other services

```bash
dotnet run --project GloboTicket.Sales
dotnet run --project GloboTicket.CustomerService
dotnet run --project GloboTicket.WebSales
```