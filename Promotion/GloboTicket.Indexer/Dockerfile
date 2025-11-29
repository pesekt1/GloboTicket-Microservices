# Build stage
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY GloboTicket.Indexer/GloboTicket.Indexer.csproj ./GloboTicket.Indexer/
COPY GloboTicket.Promotion.Messages/ ./GloboTicket.Promotion.Messages/
WORKDIR /src/GloboTicket.Indexer
RUN dotnet restore "GloboTicket.Indexer.csproj"

COPY GloboTicket.Indexer/. ./
RUN dotnet publish "GloboTicket.Indexer.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/runtime:5.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "GloboTicket.Indexer.dll"]
