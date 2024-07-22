using Markdig.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using blog_website.Data;
using blog_website.Models;
using blog_website.Models.classes;
using Westwind.AspNetCore.Markdown;

namespace blog_website;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        // Configure the database context
        builder.Services.AddDbContext<ApplicationDbCon>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("ConStr")));

        // Add authentication services
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/DataContextAdmin/Login";
                options.LogoutPath = "/DataContextAdmin/Login";
            });

        // Add authorization services
        builder.Services.AddAuthorization();

        // Add Markdown services
        builder.Services.AddMarkdown();

        var app = builder.Build();

        // Seed the database with initial data
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbCon>();
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

        // Use Markdown middleware
        app.UseMarkdown();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
