# Build stage
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY GloboTicket.Emailer/GloboTicket.Emailer.csproj ./GloboTicket.Emailer/
COPY GloboTicket.Promotion.Messages/ ./GloboTicket.Promotion.Messages/
WORKDIR /src/GloboTicket.Emailer
RUN dotnet restore "GloboTicket.Emailer.csproj"

COPY GloboTicket.Emailer/. ./
RUN dotnet publish "GloboTicket.Emailer.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/runtime:5.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "GloboTicket.Emailer.dll"]
