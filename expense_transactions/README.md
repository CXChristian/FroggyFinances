Making changes to context and models, use this to update migrations
dotnet-ef migrations add M1 -o Data/Migrations

Update the database with the new migration/model updates
dotnet ef database update
  - Cannot update when user table is already existing
