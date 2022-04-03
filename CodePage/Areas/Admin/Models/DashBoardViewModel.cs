using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodePage.Areas.Admin.Models
{
    public class DashBoardViewModel
    {
        public int BannerCount { get; set; }
        public int AboutUsCount { get; set; }
        public int AppUserCount { get; set; }
        public int CategoryCount { get; set; }
        public int GroupCount { get; set; }
        public int MessageCount { get; set; }
        public int TeacherCount { get; set; }
        public int StudentCount { get; set; }
    }
}
