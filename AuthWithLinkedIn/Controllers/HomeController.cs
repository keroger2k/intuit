using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AuthWithLinkedIn.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserProfile()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
