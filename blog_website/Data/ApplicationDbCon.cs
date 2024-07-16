using blog_website.Models;
using blog_website.Models.classes;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace blog_website.Data;

public class ApplicationDbCon : DbContext
{
    public ApplicationDbCon(DbContextOptions<ApplicationDbCon> options) : base(options)
    {
        //Database.Migrate();
    }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Script> Scripts { get; set; }
    public DbSet<Deneme> Denemes { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Admin>()
            .HasIndex(u => u.Name)
            .IsUnique();
    }
}