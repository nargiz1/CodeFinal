using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodePage.Models
{
    public class TeacherToGroup:BaseEntity
    {
        public int TeacherId { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public Teacher Teacher { get; set; }
    }
}
