using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TwitterProject.Model.Option;
using TwitterProject.Service.Option;
using TwitterProject.Utility;

namespace TwitterProject.UI.Areas.Member.Controllers
{
    public class TweetController : Controller
    {
        TweetService _tweetService;
        AppUserService _appUserService;
        public TweetController()
        {
            _tweetService = new TweetService();
            _appUserService = new AppUserService();
        }

        public ActionResult TweetAdd()
        {
           return View( _appUserService.GetActive().OrderByDescending(x => x.CreatedDate).ToList());
        }
   
        [HttpPost]
        public ActionResult TweetAdd(Tweet tweet,HttpPostedFileBase Image)
        {
            List<string> UploadedImagePaths = new List<string>();
            UploadedImagePaths = ImageUploader.UploadSingleImage(ImageUploader.OriginalProfileImagePath, Image, 1);
            tweet.ImagePath = UploadedImagePaths[0];

            if (tweet.ImagePath=="0"||tweet.ImagePath=="1"||tweet.ImagePath=="2")
            {
                tweet.ImagePath = ImageUploader.DefaultProfileImagePath;
                tweet.ImagePath = ImageUploader.DefaultXSmallProfileImage;
                tweet.ImagePath = ImageUploader.DefaulCruptedProfileImage;
            }
            else
            {
                tweet.ImagePath = UploadedImagePaths[1];
                tweet.ImagePath = UploadedImagePaths[2];
            }

            AppUser user = _appUserService.GetByDefault(x => x.UserName == User.Identity.Name);
            tweet.AppUserID = user.ID;
            tweet.CreatedDate =DateTime.Now;
           _tweetService.Add(tweet);

            return Redirect("/Member/Home/MemberHomeIndex");
      
        }

    public ActionResult Delete(Guid id)
        {
            _tweetService.Remove(id);
            return Redirect("/Member/Home/MemberHomeIndex");
        }
    }
}