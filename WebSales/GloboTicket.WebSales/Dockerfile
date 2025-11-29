# Build stage
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

ARG AZURE_DEVOPS_USERNAME
ARG AZURE_DEVOPS_PAT

# Generate nuget.config with credentials
RUN echo '<?xml version="1.0" encoding="utf-8"?>\
  <configuration>\
  <packageSources>\
  <add key="nuget.org" value="https://api.nuget.org/v3/index.json" />\
  <add key="SalesFeed" value="https://pkgs.dev.azure.com/TOMP0700/GloboTicket/_packaging/SalesFeed/nuget/v3/index.json" />\
  </packageSources>\
  <packageSourceCredentials>\
  <SalesFeed>\
  <add key="Username" value="'$AZURE_DEVOPS_USERNAME'" />\
  <add key="ClearTextPassword" value="'$AZURE_DEVOPS_PAT'" />\
  </SalesFeed>\
  </packageSourceCredentials>\
  </configuration>' > nuget.config

# Copy csproj and restore first to leverage Docker cache
COPY ["GloboTicket.WebSales.csproj", "./"]
RUN dotnet restore "GloboTicket.WebSales.csproj" --configfile nuget.config

# Copy the rest and publish
COPY . .
WORKDIR /src
RUN dotnet publish "GloboTicket.WebSales.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "GloboTicket.WebSales.dll"]