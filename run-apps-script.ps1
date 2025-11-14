# -------------------------------
# Console projects
# -------------------------------
$consoleProjects = @(
    "CustomerService/GloboTicket.CustomerService/GloboTicket.CustomerService.csproj",
    "Promotion/GloboTicket.Emailer/GloboTicket.Emailer.csproj",
    "Promotion/GloboTicket.Indexer/GloboTicket.Indexer.csproj",
    "Sales/GloboTicket.Sales/GloboTicket.Sales.csproj"
)

# Start each console app in a new window
foreach ($proj in $consoleProjects) {
    Write-Host "Starting console project: $proj"
    Start-Process "powershell.exe" -ArgumentList "-NoExit", "dotnet run --project `"$proj`""
}
