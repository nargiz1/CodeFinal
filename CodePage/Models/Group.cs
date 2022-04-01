using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodePage.Models
{
    public class Group:BaseEntity
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public int StudentCount { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<Student> Students { get; set; }
        public List<TeacherToGroup> TeacherToGroups { get; set; }
    }
}
