# Build stage
FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
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

COPY ["GloboTicket.CustomerService.csproj", "./"]
RUN dotnet restore "GloboTicket.CustomerService.csproj" --configfile nuget.config
COPY . .
RUN dotnet publish "GloboTicket.CustomerService.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "GloboTicket.CustomerService.dll"]
