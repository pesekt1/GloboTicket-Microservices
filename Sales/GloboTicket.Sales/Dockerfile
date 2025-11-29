# Build stage
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY GloboTicket.Sales/GloboTicket.Sales.csproj ./GloboTicket.Sales/
COPY GloboTicket.Sales.Messages/ ./GloboTicket.Sales.Messages/
WORKDIR /src/GloboTicket.Sales
RUN dotnet restore "GloboTicket.Sales.csproj"

COPY GloboTicket.Sales/. ./
WORKDIR /src/GloboTicket.Sales
RUN dotnet publish "GloboTicket.Sales.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/runtime:5.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "GloboTicket.Sales.dll"]
