# EXECUTING PROJECT
## WINDOWS
### Prerequisites
- Visual Studio  
![Ekran görüntüsü 2024-07-31 162013](https://github.com/user-attachments/assets/a94eebf7-ca56-4d33-8c54-68083e0444a8)  
- Microsoft.AspNetCore.Mvc.Testing, Microsoft.EntityFrameworkCore, Microsoft.EntityFrameworkCore.Design,
- Microsoft.EntityFrameworkCore.SqlServer, Microsoft.EntityFrameworkCore.Tools, Microsoft.NET.Test.Sdk,
- Markdig, Westwind.AspNetCore.Markdown, Microsoft.VisualStudio.Web.CodeGeneration.Design
> Versions can be changed

### If you want to run the project with your local mssql server database you must make some changes on appsettings.json
```
"ConnectionStrings": {
  "ConStr": "Server=yourmssqlname; Initial Catalog=yourdatabasename; Trusted_Connection=True; TrustServerCertificate=True; Integrated Security=True",
  "ConStrLinux": "Server=yourmssqlname; User Id=your_id; Password=your_password; Initial Catalog=yourdatabasename; TrustServerCertificate=True;"
}
```

Program.cs code snippet
```
// Choose configuration accordingly OS
if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
{
    // Configure the database context
    builder.Services.AddDbContext<ApplicationDbCon>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("ConStr")));
}
else if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
{
    // Configure the database context
    builder.Services.AddDbContext<ApplicationDbCon>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("ConStrLinux")));
}
else{
    throw new Exception("there is no database config!!");
}
```

Your program use MigrateDbContextExtensions.cs for migrations  
Program.cs code snippet
```
builder.Services.AddMigration<ApplicationDbCon>();
```
> If you want to add columns or change something you should use Nuget Package Manager Console  
> example: add-migrations your_migration_name
