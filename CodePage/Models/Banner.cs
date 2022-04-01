using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodePage.Models
{
    public class Banner: BaseEntity
    {
        [Required, MaxLength(200)]
        public string Title { get; set; }
        [Required]
        public string Subtitle { get; set; }
        [Required]
        public string Color { get; set; }
    }
}
