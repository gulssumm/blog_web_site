using System.ComponentModel.DataAnnotations;

namespace blog_website.Models.classes
{
    public class Admin
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string password { get; set; }
    }
}
