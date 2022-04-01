using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodePage.Models
{
    public class Student:BaseEntity
    {
        [Required, MaxLength(200)]
        public string FullName { get; set; }
        [Required]
        public string Mail { get; set; }
        [Required]
        public string Number { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public int CAP { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
