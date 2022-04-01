using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodePage.Models
{
    public class Message:BaseEntity
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [Required, MaxLength(100)]
        public string Surname { get; set; }
        [Required, MaxLength(10)]
        public string Number { get; set; }
        [Required]
        public string Department { get; set; }
    }
}
