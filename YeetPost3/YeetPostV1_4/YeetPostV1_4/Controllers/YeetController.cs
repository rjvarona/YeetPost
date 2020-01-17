using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YeetPostV1_4.Models;
using YeetPostV1_4.ViewModel;
using YeetPostV1_4.Data;


namespace YeetPostV1_4.Controllers
{
    public class YeetController : Controller
    {
        private readonly ILogger<YeetController> _logger;
        private readonly YeetServices _yeetServices = new YeetServices();
        private readonly AccountServices _accountServices = new AccountServices();

        public YeetController(ILogger<YeetController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int? id)
        {
            bool isAuthenticated = User.Identity.IsAuthenticated;

            if(!isAuthenticated)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            //var userId = User.Identity.GetUerId();

            //var model = new YeetViewModel();
            //var location = _accountServices.getLocation(); 
            //model.yeets = _yeetServices.GetYeets(location);
           

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
