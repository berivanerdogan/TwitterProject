using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TwitterProject.Model.Option;
using TwitterProject.Service.Option;
using TwitterProject.UI.Areas.Member.Models.DTO;
using TwitterProject.Utility;

namespace TwitterProject.UI.Areas.Member.Controllers
{
    public class AppUserController : Controller
    {
        AppUserService _appUserService;
        TweetService _tweetService;
        public AppUserController()
        {
            _appUserService = new AppUserService();
            _tweetService = new TweetService();
        }

        public ActionResult UserProfile()
        {
            Guid userid = _appUserService.FindByUserName(User.Identity.Name).ID;
            List<AppUser> model = _appUserService.GetDefault(x => x.ID == userid);
            return View(model);
        }
        
        public ActionResult EditProfile(Guid id)
        {
            AppUser user = _appUserService.GetByID(id);
            AppUserDTO model = new AppUserDTO();
            model.ID = user.ID;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.UserName = user.UserName;
            model.Email = user.Email;
            model.Password = user.Password;
            model.Bio = user.Bio;
            model.PhoneNumber = user.PhoneNumber;
            model.UserName = user.UserImage;
            model.XSmallUserImage = user.XSmallUserImage;
            model.CruptedUserImage = user.CruptedUserImage;
            model.Follow = user.Follow;
            model.Follower = user.Follower;

            return View(model);
        }
        [HttpPost]
        public ActionResult EditProfile(AppUser user,HttpPostedFileBase Image)
        {
            List<string> UploadedImagePaths = new List<string>();
            UploadedImagePaths = ImageUploader.UploadSingleImage(ImageUploader.OriginalProfileImagePath, Image, 1);

            user.UserImage = UploadedImagePaths[0];
            AppUser update = _appUserService.GetByID(user.ID);

            if (user.UserImage=="0"||user.UserImage=="1"||user.UserImage=="2")
            {
                if (update.UserImage==null||update.UserImage==ImageUploader.DefaultProfileImagePath)
                {
                    update.UserImage = ImageUploader.DefaultProfileImagePath;
                    update.XSmallUserImage = ImageUploader.DefaultXSmallProfileImage;
                    update.CruptedUserImage = ImageUploader.DefaulCruptedProfileImage;
                }
                else
                {
                    update.UserImage = update.UserImage;
                    update.XSmallUserImage = update.XSmallUserImage;
                    update.CruptedUserImage = update.CruptedUserImage;
                }
            }
            else
            {
                update.UserImage = UploadedImagePaths[0];
                update.XSmallUserImage = UploadedImagePaths[1];
                update.CruptedUserImage = UploadedImagePaths[2];
            }

            _appUserService.Update(user);
            return Redirect("/Member/AppUser/EditProfile");
        }
    }
}