using blog_website.Controllers;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace blog_website.Models.classes
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Substance { get; set; }
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
        public int UserId { get; set; } // Foreign key of type int


        public User? User { get; set; } // Navigation property
    }
}
