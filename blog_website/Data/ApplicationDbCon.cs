using blog_website.Models;
using Microsoft.EntityFrameworkCore;
namespace blog_website.Data
{
    public class ApplicationDbCon:DbContext
    {
        public ApplicationDbCon(DbContextOptions<ApplicationDbCon> options): base(options)
        {

        }
        public DbSet<Models.classes.Admin> Admins { get; set; } = null!;
    }
}
