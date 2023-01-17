using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        SchoolContext db = new SchoolContext();
        public ActionResult Index()
        {
            db.Students.ToList();
            db.Grades.ToList();
            return View();
        }
    }
}