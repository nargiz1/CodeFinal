using CodePage.DAL;
using CodePage.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodePage.Controllers
{
    public class MessageController : Controller
    {
        private readonly AppDbContext db;
        public MessageController(AppDbContext _db)
        {
            db = _db;
        }
        public IActionResult Add(string Name, string Surname, string NumberBeginning, string Number, string Department)
        {
            if (Name != null && Surname!=null && Number != null)
            {
                Message ms = new Message()
                {
                    Name = Name,
                    Surname = Surname,
                    Number = NumberBeginning + Number,
                    Department = Department,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                };
                db.Messages.Add(ms);
                db.SaveChanges();
                TempData["messageAdded"] = "Your message was added successsfully";
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
