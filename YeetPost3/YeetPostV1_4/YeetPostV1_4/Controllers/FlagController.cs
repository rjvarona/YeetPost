using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using YeetPostV1_4.Data;
using YeetPostV1_4.ViewModel;

namespace YeetPostV1_4.Controllers
{
    public class FlagController : Controller
    {

        private readonly FlagServices _flagServices = new FlagServices();
        private readonly YeetServices _yeetServices = new YeetServices();

        public string FlagPost(string yeetID, List<string> whoFlags, bool remove, string reason, string location)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            //put into a class later and pass it through much cleaner
            var userId = claim.Value;

            //move get the yeets to shared and update if its by trend or what not.
            _flagServices.flagPost(yeetID, userId, whoFlags, remove, reason);

            var model = new YeetViewModel();
            model.yeets = _yeetServices.GetYeetsByTrend(location, userId);
            model.location = location;

            return new JavaScriptSerializer().Serialize(model);
        }
    }
}