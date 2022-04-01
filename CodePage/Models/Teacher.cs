using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodePage.Models
{
    public class Teacher:BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Mail { get; set; }
        [Required]
        public string Number { get; set; }
        public List<TeacherToGroup> TeacherToGroups { get; set; }
    }
}
