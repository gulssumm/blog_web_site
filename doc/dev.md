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
- You need to install Jenkins on your machine
> You can get help with this link: https://www.jenkins.io/doc/book/installing/windows/
- You must login Jenkins and create a Github app
- Generate private key and convert it
- You can use this command to convert:
```
   cd~/Downloads
   openssl pkcs8 -topk8 -inform PEM -outform PEM -in jenkins_name.year-mont-day.private-key.pem -out converted-github-app.pem -nocrypt
   ll converted github-app.pem
```
- Then install the Github app
- Go to credentials and select your project's global option
- Create a credential and use the key that come from your Github app
- Go to dashboard and add an item
> You can get help from this video: https://www.youtube.com/watch?v=LbXKUKQ24T8
- When the scanning finished successfully you can go to localhost and open the project

## UBUNTU 22.04
> I run on VirtualBox and write according to this
### If you want to run the project with your local mssql server database you must make install mssql on your machine
```
    sudo apt-get update  
    sudo apt-get upgrade  
    sudo apt install curl  
    sudo apt-get update  
    curl -fsSL https://packages.microsoft.com/keys/microsoft.asc | sudo gpg --dearmor -o /usr/share/keyrings/microsoft-prod.gpg  
    curl -fsSL https://packages.microsoft.com/config/ubuntu/22.04/mssql-server-2022.list | sudo tee /etc/apt/sources.list.d/mssql-server-2022.list  
    sudo apt-get update  
    sudo apt-get install -y mssql-server  
    sudo /opt/mssql/bin/mssql-conf setup  
    systemctl status mssql-server --no-pager  
    curl https://packages.microsoft.com/keys/microsoft.asc | sudo tee /etc/apt/trusted.gpg.d/microsoft.asc  
    curl https://packages.microsoft.com/config/ubuntu/22.04/prod.list | sudo tee /etc/apt/sources.list.d/mssql-server-2022.list  
    sudo apt-get update  
    sudo apt-get install mssql-tools18  
```
> You can get help with this link: https://learn.microsoft.com/en-us/sql/linux/sql-server-linux-setup-tools?view=sql-server-ver16&tabs=ubuntu-install
> Additionally, you must install dotnet on your machine to execute the project
```
    sudo apt-get update && \
    sudo apt-get install -y dotnet-sdk-8.0
    sudo apt-get update && \
    sudo apt-get install -y aspnetcore-runtime-8.0
```
> If you haven't executed the project, you may add .bashrc to this paths:  
> export PATH="$PATH:/opt/mssql-tools18/bin"  
> export PATH="$PATH:/home/your_name/.dotnet/tools"  
> You can use this commad to enter .bashrc: sudo nano ~/.bashrc
### If you want to run the project on Jenkins
- You need to install Jenkins on your machine
> You can get help with this link: https://www.jenkins.io/doc/book/installing/linux/
- You can do the same things that I mentioned earlier
