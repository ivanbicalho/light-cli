$ErrorActionPreference = "Stop"

rm publish -r
dotnet build .
dotnet pack -c Release -o publish
cd publish
$key=[System.Environment]::GetEnvironmentVariable('NUGET_API_KEY').ToString()
nuget push *.nupkg -Source https://api.nuget.org/v3/index.json -ApiKey $key