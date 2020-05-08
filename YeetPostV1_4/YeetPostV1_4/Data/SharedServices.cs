using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YeetPostV1_4.Data;
using YeetPostV1_4.ViewModel;
using System.Security.Claims;

namespace YeetPostV1_4.Data
{
    public class SharedServices : Controller
    {
        private readonly YeetServices _yeetServices = new YeetServices();


        public YeetViewModel GetYeets(string location)
        {

            var model = new YeetViewModel();
            model.yeets = _yeetServices.GetYeetsByNew(location, getUserId());
            model.location = location;


            return model;
        }

        /// <summary>
        /// supposed to get User Id currently Not working
        /// </summary>
        /// <returns></returns>
        public string getUserId()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            return claim.Value;

        }

    }
}


