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
    public class UserInteraction : Controller
    {
        private readonly CommentServices _commentServices = new CommentServices();

        private readonly YeetServices _yeetServices = new YeetServices();

        private readonly LikeServices _likeServices = new LikeServices();

        private readonly FlagServices _flagServices = new FlagServices();

        //this is the first yeet ever
        public IActionResult ViewComments(string yeetID)
        {

            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            string name = User.Identity.Name;

            //put into a class later and pass it through much cleaner
            var userId = claim.Value;


            var model = _commentServices.getComment(yeetID, userId);
            model.showComments = true;

            return View(model);
        }


        [HttpPost]
        public string addComment(string comment, string yeetID)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            string name = User.Identity.Name;

            //put into a class later and pass it through much cleaner
            var userId = claim.Value;

            _commentServices.newComment(name, comment, userId, yeetID);

            var model = _commentServices.getComment(yeetID, userId);


            var x = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            return x;
        }

        [HttpPost]
        public string deleteYeet(string yeetID)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            string name = User.Identity.Name;

            //put into a class later and pass it through much cleaner
            string userId = claim.Value;
            _yeetServices.deleteYeet(yeetID);


            var model = new CommentViewModel();
            model.yeet = new DataModel.Yeet();
            model.showComments = false;


            return new JavaScriptSerializer().Serialize(model);
        }



        public string FlagPost(string yeetID, List<string> whoFlags, bool remove, string reason, string location, string status)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            //put into a class later and pass it through much cleaner
            var userId = claim.Value;

            //move get the yeets to shared and update if its by trend or what not.
            _flagServices.flagPost(yeetID, userId, whoFlags, remove, reason);

            var model = new YeetViewModel();

            //filter by new 
            model.yeets = (status == "new") ? _yeetServices.GetYeetsByNew(location, userId) : _yeetServices.GetYeetsByTrend(location, userId);

            model.status = status;
            model.location = location;

            return new JavaScriptSerializer().Serialize(model);
        }

        public string LikePost(string yeetID, List<string> whoLikes, string location, bool remove, string status, string from)
        {


            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            //put into a class later and pass it through much cleaner
            var userId = claim.Value;

            //move get the yeets to shared and update if its by trend or what not.
            _likeServices.likePost(yeetID, userId, whoLikes, remove);

            var model = new YeetViewModel();


            var yeet = _commentServices.getYeetAsync(yeetID, userId);


            return new JavaScriptSerializer().Serialize(yeet);
        }
    }
}
