using blog_website.Controllers;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace blog_website.Models.classes
{
    class MyContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Admin>()
                .HasIndex(u => u.Name)
                .IsUnique(true);
        }
    }
    public class Admin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
