using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodePage.Models
{
    public class AppUser: IdentityUser
    {
        [Required, MaxLength(200)]
        public string FullName { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
