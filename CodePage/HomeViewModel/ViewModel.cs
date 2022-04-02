using CodePage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodePage.HomeViewModel
{
    public class ViewModel
    {
        public List<Banner> Banners { get; set; }
        public List<Category> Categories { get; set; }
        public List<AboutUs> AboutUs { get; set; }
    }
}
