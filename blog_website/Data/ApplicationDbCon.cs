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
    public DbSet<User> Users { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Deneme> Denemes { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>()
            .HasIndex(u => u.Name)
            .IsUnique();
    }
}