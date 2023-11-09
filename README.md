# Change Data Capture Test

This is a sample project to test out Microsoft SQL Server's "change data capture" 
functionality in order to publish events to a service bus.

## UserEditor

Mock user editing application, backed by Microsoft SQL Server, as a test application 
for modifying user accounts

## Setup

Run the [`DatabaseCreation.sql`](db/DatabaseCreation.sql) script to create the
sample database and seed it with sample data.

## Docker

If you want to run Microsoft SQL Server in a docker container, use the following command

```
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourStrong(!)Password" -p 1433:1433 --name sql-server-2022 -d mcr.microsoft.com/mssql/server:2022-latest
```