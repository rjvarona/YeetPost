using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YeetPostV1_4.DataModel
{
    public class Yeet
    {
        public string date;

        public string Guid; //user

        public string header;

        public string totalLikes;

        public string username;

        public List<string> whoLikes;
        
        public List<string> whoFlags;

        public string yeetID;

        public string yeet;

        public string location;

        public bool? iLiked;

        public bool? iFlagged;

        public bool? isMine;
        //modal for flag
        public bool? modal;
        //modal for deletion
        public bool? deleteModal;
    }
}
