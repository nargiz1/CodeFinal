using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodePage.Models
{
    public class AboutUs:BaseEntity
    {
        [Required, MaxLength(300)]
        public string Title  { get; set; }
        public string Text  { get; set; }
        public string Image  { get; set; }
        [NotMapped]
        public IFormFile ImageFile  { get; set; }
    }
}
