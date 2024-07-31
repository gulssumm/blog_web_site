# EXECUTING PROJECT
## WINDOWS
### Prerequisites
- Visual Studio  
![Ekran görüntüsü 2024-07-31 162013](https://github.com/user-attachments/assets/a94eebf7-ca56-4d33-8c54-68083e0444a8)  
- Microsoft.AspNetCore.Mvc.Testing, Microsoft.EntityFrameworkCore, Microsoft.EntityFrameworkCore.Design,
- Microsoft.EntityFrameworkCore.SqlServer, Microsoft.EntityFrameworkCore.Tools, Microsoft.NET.Test.Sdk,
- Markdig, Westwind.AspNetCore.Markdown, Microsoft.VisualStudio.Web.CodeGeneration.Design
- 
> You can clone my repository
> Nuget package versions can be changed  

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
After all, you can just press the https button on Visual Studio or you can write 'dotnet run' on cmd to execute the project

### If you want to run the project on Jenkins
- You must login Jenkins and create a Github app
- Generate private key and convert it
- You can use this command to convert: 1-cd~/Downloads
- 2-openssl pkcs8 -topk8 -inform PEM -outform PEM -in jenkins_name.year-mont-day.private-key.pem -out converted-github-app.pem -nocrypt
- 3-ll converted github-app.pem
- Then install the Github app
- Go to credentials and select your project's global option
- Create a credential and use the key that come from your Github app
- Go to dashboard and add an item
> You can get help from this video: https://www.youtube.com/watch?v=LbXKUKQ24T8
- When the scanning finished successfull you can go to localhost and open the project

## UBUNTU 22.04
> I run on VirtualBox and write according to this
### If you want to run the project with your local mssql server database you must make install mssql on your machine
```
    1  sudo apt-get update  
    2  sudo apt-get upgrade  
    3  sudo apt install curl  
    4  sudo apt-get update  
    5  curl -fsSL https://packages.microsoft.com/keys/microsoft.asc | sudo gpg --dearmor -o /usr/share/keyrings/microsoft-prod.gpg  
    6  curl -fsSL https://packages.microsoft.com/config/ubuntu/22.04/mssql-server-2022.list | sudo tee /etc/apt/sources.list.d/mssql-server-2022.list  
    7  sudo apt-get update  
    8  sudo apt-get install -y mssql-server  
    9  sudo /opt/mssql/bin/mssql-conf setup  
    10 systemctl status mssql-server --no-pager  
    11 curl https://packages.microsoft.com/keys/microsoft.asc | sudo tee /etc/apt/trusted.gpg.d/microsoft.asc  
    12 curl https://packages.microsoft.com/config/ubuntu/22.04/prod.list | sudo tee /etc/apt/sources.list.d/mssql-server-2022.list  
    13 sudo apt-get update  
    14 sudo apt-get install mssql-tools18  
```
