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
    public class CommentController : Controller
    {
        
        private readonly CommentServices _commentServices = new CommentServices();

        private readonly YeetServices _yeetServices = new YeetServices();

        //this is the first yeet ever
        public IActionResult ViewComments(string yeetID)
        {

            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            string name = User.Identity.Name;

            //put into a class later and pass it through much cleaner
            var userId = claim.Value;


            var model = _commentServices.getComment(yeetID,  userId);
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


    }
}