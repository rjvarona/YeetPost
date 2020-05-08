using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YeetPostV1_4.Data;
using YeetPostV1_4.ViewModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YeetPostV1_4.Controllers
{
    public class ProfileController : Controller
    {


        private readonly YeetServices _yeetServices = new YeetServices();
        private readonly AccountServices _accountServices = new AccountServices();
        
        public IActionResult Profile()
        {
            bool isAuthenticated = User.Identity.IsAuthenticated;


            if (!isAuthenticated)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }


            string userId = getUserId();

            var model = new YeetViewModel();

            var status = _accountServices.getStatus(userId);
            if (status == "banned")
            {
                return RedirectToAction("Banned", "Status");
            }



            model.yeets = _yeetServices.GetYeetsById(userId);

            return View(model);
        }



        public string getUserId()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            return claim.Value;

        }
    }
}
