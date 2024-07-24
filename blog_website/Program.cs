using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using blog_website.Data;
using myLib;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using System.Runtime.InteropServices;

namespace blog_website;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddTransient<IMarkDown, MarkDown>();

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

        // Add authentication services
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/DataContextAdmin/Login";
                options.LogoutPath = "/DataContextAdmin/Login";
            });

        builder.Services.AddDataProtection().UseCryptographicAlgorithms(
            new AuthenticatedEncryptorConfiguration
            {
                EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
            });

        // Add authorization services
        builder.Services.AddAuthorization();

        var app = builder.Build();

        // Seed the database with initial data
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbCon>();
            context.Database.Migrate();
        }

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        // If you use default files make sure you do it before markdown middleware
        app.UseDefaultFiles(new DefaultFilesOptions()
        {
            DefaultFileNames = new List<string> { "index.md", "index.html" }
        });

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
