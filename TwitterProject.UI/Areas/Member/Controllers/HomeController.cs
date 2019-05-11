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
            model.Tweets = _tweetService.GetActive();

            foreach (var item in model.Tweets)
            {
                model.Comments = _commentService.GetDefault(x => x.TweetID == item.ID).OrderByDescending(X => X.CreatedDate).ToList();

                model.LikeCount = _likeService.GetDefault(x => x.TweetID == item.ID).Count();
                model.CommentCount = _commentService.GetDefault(x => x.TweetID == item.ID).Count();
            }

            return View(model);
        }
    }
}