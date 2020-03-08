using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}