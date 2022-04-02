using CodePage.DAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodePage.Controllers
{
    public class AboutUsController : Controller
    {
        private readonly AppDbContext db;
        public AboutUsController(AppDbContext _db)
        {
            db = _db;
        }
    }
}
