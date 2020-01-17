using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YeetPostV1_4.Models;
using YeetPostV1_4.ViewModel;
using YeetPostV1_4.Data;
//using Microsoft.AspNet.Identity; // NuGet: Microsoft ASP.NET Identity Core.
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

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

            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var userId = claim.Value;

            var model = new YeetViewModel();
            var location = _accountServices.getLocation(userId);
            model.yeets = _yeetServices.GetYeets(location);


            return View(model);
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
