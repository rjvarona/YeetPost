using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YeetPostV1_4.DataModels;
using YeetPostV1_4.DataModel;
using Nancy.Json;

namespace YeetPostV1_4.ViewModel
{
    public class YeetViewModel
    {
        public List<Yeet> yeets { get; set; }

        public string location { get; set; }

        public string yeetModel { get; set; }

     }
}
