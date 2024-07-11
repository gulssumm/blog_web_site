using System.ComponentModel.DataAnnotations;

namespace blog_website.Models.classes
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
