﻿using blog_website.Controllers;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace blog_website.Models.classes
{
    public class Admin
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Username is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
