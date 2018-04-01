using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApplication3.Model;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {   
            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }


        private readonly CollectedUsersSettings _CollectedUsersSettings;

        public HomeController(IOptions<CollectedUsersSettings> collectedUsersSettings)
        {
            _CollectedUsersSettings = collectedUsersSettings.Value;
        }


        public IActionResult Users()
        {
            return Json(_CollectedUsersSettings.Users);
        }
    }
}
