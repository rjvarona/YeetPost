using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace YeetPost.Controllers
{
    public class OAuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }



        [HttpGet]
        [Route("/oauth/google")]
        public IActionResult GoogleLogin()
        {
            return Challenge(new AuthenticationProperties { RedirectUri = "Account" }, "Google");
        }
    }
}