using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YeetPostV1_4.DataModel
{
    public class Flag
    {
        public string userID { get; set; }
        public string yeetID { get; set; }
        public List<string> whoFlags { get; set; }
    }
}
