using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YeetPostV1_4.DataModel;

namespace YeetPostV1_4.ViewModel
{
    public class CommentViewModel
    {
        public List<Comments> comments { get; set; }
        public Yeet yeet { get; set; }

        public bool? showComments { get; set; }
    }
}
