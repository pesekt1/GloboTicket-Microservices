# Build stage
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

# Copy csproj and restore first to leverage Docker cache
COPY GloboTicket.Promotion/GloboTicket.Promotion.csproj ./GloboTicket.Promotion/
COPY GloboTicket.Promotion.Messages/ ./GloboTicket.Promotion.Messages/
WORKDIR /src/GloboTicket.Promotion
RUN dotnet restore "GloboTicket.Promotion.csproj"

# Copy the rest and publish
COPY GloboTicket.Promotion/. ./
WORKDIR /src/GloboTicket.Promotion
RUN dotnet publish "GloboTicket.Promotion.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "GloboTicket.Promotion.dll"]
