using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YeetPost2.Models;
using YeetPost2.ViewModel;
using YeetPost2.Data;

namespace YeetPost2.Controllers
{
    public class HomeController : Controller
    {

        private readonly AccountServices _accountService = new AccountServices();
        public IActionResult Index()
        {

            var model = new AccountViewModel
            {
                accounts = _accountService.GetUsers()
            };

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
