$_migrationName = $Args[0]

if ($_migrationName -eq $null)
{
	write-host "Migration name is required" -fore red
	write-host "Example:" -fore red
	write-host "      database_migration.ps1 <migration name>" -fore blue
	write-host "      database_migration.ps1 'some name'" -fore blue
}
else
{
	dotnet ef migrations add $_migrationName -v --project "Server\" --output-dir "Data\Migrations"
}
