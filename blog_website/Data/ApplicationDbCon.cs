using blog_website.Models.classes;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace blog_website.Data;

public class ApplicationDbCon : DbContext
{
    public ApplicationDbCon(DbContextOptions<ApplicationDbCon> options) : base(options)
    {
        Database.Migrate();
    }
    public DbSet<Admin> Admins { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Admin>()
            .HasIndex(u => u.Name)
            .IsUnique();
    }
}