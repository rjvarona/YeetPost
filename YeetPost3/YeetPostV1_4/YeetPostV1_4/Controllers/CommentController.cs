using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YeetPostV1_4.Data;

namespace YeetPostV1_4.Controllers
{
    public class CommentController : Controller
    {
        
        private readonly CommentServices _commentServices = new CommentServices();


        //this is the first yeet ever
        public IActionResult ViewComments(string yeetID)
        {
            var model = _commentServices.getComment(yeetID);

            
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

            var model = _commentServices.getComment(yeetID);
      

            var x = Newtonsoft.Json.JsonConvert.SerializeObject(model);

            return x;
        }


    }
}