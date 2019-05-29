using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TwitterProject.Model.Option;
using TwitterProject.Service.Option;
using TwitterProject.UI.Areas.Member.Models.VM;
using TwitterProject.Utility;

namespace TwitterProject.UI.Areas.Member.Controllers
{
    public class TweetController : Controller
    {
        TweetService _tweetService;
        AppUserService _appUserService;
        CommentService _commentService;
        LikeService _likeService;

        public TweetController()
        {
            _tweetService = new TweetService();
            _appUserService = new AppUserService();
            _commentService = new CommentService();
            _likeService = new LikeService();
        }

        public ActionResult TweetAdd()
        {
            Guid userid = _appUserService.FindByUserName(User.Identity.Name).ID;
            TweetDetailVM model = new TweetDetailVM()
            {
                Tweets = _tweetService.GetDefault(x => x.AppUserID == userid && (x.Status == TwitterProject.Core.Enum.Status.Active || x.Status == TwitterProject.Core.Enum.Status.Updated)),
                AppUsers = _appUserService.GetDefault(x => x.ID == userid)
            };
            return View(model);
            //Guid userid = _appUserService.FindByUserName(User.Identity.Name).ID;
            //return View( _tweetService.GetDefault(x=>x.AppUserID==userid && (x.Status==TwitterProject.Core.Enum.Status.Active|| x.Status == TwitterProject.Core.Enum.Status.Updated)).OrderByDescending(x => x.CreatedDate).ToList());
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
            Tweet tweet = _tweetService.GetByID(id);
            if (tweet.AppUser.UserName==User.Identity.Name)
            {
                _tweetService.Remove(id);
                return Redirect("/Member/Home/MemberHomeIndex");
            }            
            return Redirect("/Member/Home/MemberHomeIndex");
        }

        public ActionResult Detail(Guid id)
        {
            TweetDetailVM model = new TweetDetailVM();
            model.Tweet = _tweetService.GetByID(id);
            model.AppUser = _appUserService.GetByID(model.Tweet.AppUser.ID);
            model.Comments = _commentService.GetDefault(x => x.TweetID == id && (x.Status == Core.Enum.Status.Active || x.Status == Core.Enum.Status.Updated));
            model.LikeCount = _likeService.GetDefault(x => x.TweetID == id && (x.Status == Core.Enum.Status.Active || x.Status == Core.Enum.Status.Updated)).Count;
            model.CommentCount = _commentService.GetDefault(x => x.TweetID == id && (x.Status == Core.Enum.Status.Active || x.Status == Core.Enum.Status.Updated)).Count;
            model.Likes = _likeService.GetDefault(x => x.TweetID == id && (x.Status == Core.Enum.Status.Active || x.Status == Core.Enum.Status.Updated));


            return View(model);
        }
    }
}