using blog_website.Models.classes;
using Microsoft.EntityFrameworkCore;

namespace blog_website.Data;

public class ApplicationDbCon : DbContext
{
    public ApplicationDbCon(DbContextOptions<ApplicationDbCon> options) : base(options)
    {
        Database.Migrate();
    }

    public DbSet<Admin> Admins { get; set; } = null!;
}