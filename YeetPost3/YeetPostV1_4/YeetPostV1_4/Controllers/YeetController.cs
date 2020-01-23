﻿using System;
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
using Newtonsoft.Json;
//using Microsoft.AspNet.Identity; // NuGet: Microsoft ASP.NET Identity Core.
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Nancy.Json;

namespace YeetPostV1_4.Controllers
{
    public class YeetController : Controller
    {
        private readonly ILogger<YeetController> _logger;
        private readonly YeetServices _yeetServices = new YeetServices();
        private readonly AccountServices _accountServices = new AccountServices();
        public string location;
        private string userId;

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

            string name = User.Identity.Name;


            userId = claim.Value;

            var model = new YeetViewModel();
            location = _accountServices.getLocation(userId);
            model.yeets = _yeetServices.GetYeets(location);
            model.location = location;
            var x = JsonConvert.SerializeObject(model);

            return View(model);
        }



        [HttpPost]
        public string pushNewYeet(string header, string yeet)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            string name = User.Identity.Name;

            //put into a class later and pass it through much cleaner
            userId = claim.Value;

            location = _accountServices.getLocation(userId);


            _yeetServices.newYeet(header, yeet, location, userId, name);

            var model = new YeetViewModel();
            model.yeets = _yeetServices.GetYeets(location);
            model.location = location;


            return new JavaScriptSerializer().Serialize(model);
        }


        public string getYeets(string location)
        {
            var model = new YeetViewModel();
            model.yeets = _yeetServices.GetYeets(location);

            return new JavaScriptSerializer().Serialize(model);
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
