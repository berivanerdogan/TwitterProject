using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TwitterProject.Model.Option;
using TwitterProject.Service.Option;
using TwitterProject.UI.Areas.Member.Models.VM;

namespace TwitterProject.UI.Areas.Member.Controllers
{
    public class HomeController : Controller
    {
        AppUserService _appUserService;
        TweetService _tweetService;
        CommentService _commentService;
        LikeService _likeService;
        public HomeController()
        {
            _appUserService = new AppUserService();
            _tweetService = new TweetService();
            _commentService = new CommentService();
            _likeService = new LikeService();
        }
        public ActionResult MemberHomeIndex()
        {
            TweetDetailVM model = new TweetDetailVM();
             AppUser user = _appUserService.GetByDefault(x => x.UserName == User.Identity.Name);
            model.Tweets = _tweetService.GetActive().OrderByDescending(x=>x.CreatedDate).ToList();

            foreach (var item in model.Tweets)
            {
              
                model.Tweet = _tweetService.GetByID(item.ID);
                model.AppUser = _appUserService.GetByID(model.Tweet.AppUser.ID);
                model.Comments = _commentService.GetDefault(x => x.TweetID == item.ID);
                model.Likes = _likeService.GetDefault(x => x.TweetID == item.ID);
                model.CommentCount = _commentService.GetDefault(x => x.TweetID == item.ID).Count();
                model.LikeCount = _likeService.GetDefault(x => x.TweetID == item.ID).Count();
            }

            return View(model);
        }

    }
}