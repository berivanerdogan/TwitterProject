using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwitterProject.UI.Areas.Member.Models.DTO
{
    public class AppUserDTO
    {
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public string UserImage { get; set; }
        public int Follow { get; set; }
        public int Follower { get; set; }
        public string XSmallUserImage { get; set; }
        public string CruptedUserImage { get; set; }

        public Guid CommentID { get; set; }
        public Guid TweetID { get; set; }
        public Guid LikeID { get; set; }


    }
}