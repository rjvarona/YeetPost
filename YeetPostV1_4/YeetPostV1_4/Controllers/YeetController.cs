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
using YeetPostV1_4.DataModel;
//master



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


        public IActionResult Index()
        {
            bool isAuthenticated = User.Identity.IsAuthenticated;
            var x = 2;

            if (!isAuthenticated)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }


            string userId = getUserId();
            var status = _accountServices.getStatus(userId);
            if(status == "banned")
            {
                return RedirectToAction("Banned", "Status");
            }

            var model = new YeetViewModel();
            location = _accountServices.getLocation(userId);
            
            model.yeets = _yeetServices.GetYeetsByNew(location, userId);
            model.status = "new";
            

            model.location = location;

            //return RedirectToAction("ViewComments", "Comment", new { yeetID = "ZFfMisCGPO0I7sK1HaDE" });

            return View(model);
        }

        public string getUserId()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            return claim.Value;

        }




        [HttpPost]
        public string pushNewYeet(string header, string yeet, string location)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            string name = User.Identity.Name;

            //put into a class later and pass it through much cleaner
            userId = claim.Value;

           

            _yeetServices.newYeet(header, yeet, location, userId, name);

            var model = new YeetViewModel();
            model.yeets = _yeetServices.GetYeetsByNew(location, userId);
            model.location = location;


            var x = JsonConvert.SerializeObject(model);

            return x;
        }


        [HttpGet]
        public string filterBy(string location, string byWhat)
        {
            

            //put into a class later and pass it through much cleaner
            userId = getUserId();


            var model = new YeetViewModel();
            if (byWhat == "new")
            { 
                model.yeets = _yeetServices.GetYeetsByNew(location, userId);
                model.status = "new";
            }
            else if(byWhat == "trending")
            {
                model.yeets = _yeetServices.GetYeetsByTrend(location, userId);
                model.status = "trending";

            }
            //set it to 0
            //if(model.yeets.Count() == 0)
            //{
            //    model.yeets = new List<Yeet>();
            //    model.yeets.Add(new Yeet(
                    
            //        ));
            //}

            model.location = location;

            

            return new JavaScriptSerializer().Serialize(model);
        }



        public string getYeets(string location)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            string name = User.Identity.Name;

            //put into a class later and pass it through much cleaner
            userId = claim.Value;

            var model = new YeetViewModel();
            model.yeets = _yeetServices.GetYeetsByNew(location, userId);
            model.status = "new";

            return new JavaScriptSerializer().Serialize(model);
        }


        public string deleteYeet(string yeetId, string location, string status, string from)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            string name = User.Identity.Name;

            //put into a class later and pass it through much cleaner
            userId = claim.Value;
            _yeetServices.deleteYeet(yeetId);


            var model = new YeetViewModel();
            
            model.yeets = (status == "new") ? _yeetServices.GetYeetsByNew(location, userId) : _yeetServices.GetYeetsByTrend(location, userId);
            model.status = status;
            //from profile return to profile
            model.yeets = (from == "profile") ? _yeetServices.GetYeetsById(userId) : model.yeets;

            


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
