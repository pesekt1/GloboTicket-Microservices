docker run -d -p 9200:9200 -e "discovery.type=single-node" `
-v esdata:/usr/share/elasticsearch/data `
docker.elastic.co/elasticsearch/elasticsearch:7.9.3

docker run -d -p 15672:15672 -p 5672:5672 masstransit/rabbitmq

docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Pass@word1' -p 1433:1433 `
-v sqlvolume:/var/opt/mssql `
-d mcr.microsoft.com/mssql/server:2019-latest