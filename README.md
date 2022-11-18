# Futsal Reservation
 Futsal Reservation System using ASP .NET Core MVC

Basic project installation steps:
```
Clone repository then follow the following steps

1. navigate to directory where you cloned this repo
   
2. dotnet restore

3. navigate to FutsalReservation/appsettings.json and replace connection string with you own
   
4. create migration file
   dotnet ef migrations add FirstMigration

5. update database
   dotnet ef database update FirstMigration
   
```
