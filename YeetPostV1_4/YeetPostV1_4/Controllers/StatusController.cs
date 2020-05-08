using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace YeetPostV1_4.Controllers
{
    public class StatusController : Controller
    {
        public IActionResult Banned()
        {
            return View();
        }
    }
}