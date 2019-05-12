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
   

        public JsonResult TweetAdd(Tweet tweet,HttpPostedFileBase Image)
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

            bool isAdded = false;
            try
            {
                _tweetService.Add(tweet);
                isAdded = true;
            }
            catch (Exception ex)
            {
                isAdded = false;
                throw;
            }
            return Json(isAdded, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTweet()
        {
            //Guid appUserID = new Guid(id);
            AppUser appUser = _appUserService.GetByDefault(x=>x.UserName==User.Identity.Name);

            Tweet tweet = _tweetService.GetDefault(x => x.AppUserID == appUser.ID && x.Status == Core.Enum.Status.Active).LastOrDefault();

            return Json(new
            {
                AppUserImagePath = tweet.AppUser.UserImage,
                FirstName = tweet.AppUser.FirstName,
                LastName = tweet.AppUser.LastName,
                CreatedDate = tweet.CreatedDate.ToString(),
                Content = tweet.Content,
            }, JsonRequestBehavior.AllowGet);
        }
    }
}