Making changes to context and models, use this to update migrations
  dotnet ef migrations add M1 --context UserContext -o Data/Migrations/UserContextMigrations   

  dotnet ef migrations add M1 --context BucketContext -o Data/Migrations/BucketContextMigrations


doUpdate the database with the new migration/model updates
  dotnet ef database update