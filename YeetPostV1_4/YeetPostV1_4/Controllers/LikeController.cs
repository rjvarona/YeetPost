using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Newtonsoft.Json;
using YeetPostV1_4.Data;
using YeetPostV1_4.ViewModel;

namespace YeetPostV1_4.Controllers
{
    public class LikeController : Controller
    {
        
        private readonly LikeServices _likeServices = new LikeServices();
        private readonly YeetServices _yeetServices = new YeetServices();
        private readonly CommentServices _commentServices = new CommentServices();

        public string LikePost(string yeetID, List<string> whoLikes, string location, bool remove, string status, string from)
        {
           

            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            //put into a class later and pass it through much cleaner
            var userId = claim.Value;

            //move get the yeets to shared and update if its by trend or what not.
            _likeServices.likePost(yeetID, userId, whoLikes, remove);
                
            var model = new YeetViewModel();

           ////filter by new 
           // model.yeets = (status == "new") ? _yeetServices.GetYeetsByNew(location, userId) : _yeetServices.GetYeetsByTrend(location, userId);

           // model.status = status;
           // model.location = location;
           // //from profile
           // model.yeets = (from == "profile") ? _yeetServices.GetYeetsById(userId) : model.yeets;

            //get the yeet
            var yeet = _commentServices.getYeetAsync(yeetID, userId);


            return new JavaScriptSerializer().Serialize(yeet);
        }
    }
}