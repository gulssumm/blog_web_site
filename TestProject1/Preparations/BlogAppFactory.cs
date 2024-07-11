using blog_website;
using blog_website.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;

namespace TestProject1.Preparations;

public class BlogAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    public readonly MsSqlContainer MyDatabase = new MsSqlBuilder().Build();

    public async Task InitializeAsync()
    {
        await MyDatabase.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await MyDatabase.DisposeAsync().AsTask();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(
            o =>
            {
                o.Remove(o.Single(p => typeof(DbContextOptions<ApplicationDbCon>) == p.ServiceType));
                o.AddDbContext<ApplicationDbCon>(
                    optionsBuilder =>
                    {
                        optionsBuilder.EnableSensitiveDataLogging();
                        optionsBuilder.EnableDetailedErrors();
                        string connectionString = MyDatabase.GetConnectionString();
                        optionsBuilder.UseSqlServer(connectionString);
                    },
                    ServiceLifetime.Singleton);
            });
    }
}