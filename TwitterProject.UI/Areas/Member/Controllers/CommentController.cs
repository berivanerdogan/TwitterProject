using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TwitterProject.Model.Option;
using TwitterProject.Service.Option;

namespace TwitterProject.UI.Areas.Member.Controllers
{
    public class CommentController : Controller
    {
        CommentService _commentService;
        AppUserService _appUserService;
        TweetService _tweetService;
        LikeService _likeService;

        public CommentController()
        {
            _commentService = new CommentService();
            _appUserService = new AppUserService();
            _tweetService = new TweetService();
            _likeService = new LikeService();
        }

        
        public JsonResult AddComment(string userComment,Guid id)
        {
            Comment comment = new Comment();

            comment.AppUserID = _appUserService.FindByUserName(HttpContext.User.Identity.Name).ID;
            comment.TweetID = id;
            comment.Content = userComment;

            bool isAdded = false;
            try
            {
                _commentService.Add(comment);
                isAdded = true;
            }
            catch (Exception ex)
            {
                isAdded = false;
            }

            return Json(isAdded, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTweetComment(string id)
        {
            Guid tweetID = new Guid(id);           
            Comment comment = _commentService.GetDefault(x => x.TweetID == tweetID && x.Status == Core.Enum.Status.Active).LastOrDefault();

            return Json(new
            {
                AppUserImagePath = comment.AppUser.UserImage,
                FirstName = comment.AppUser.FirstName,
                LastName = comment.AppUser.LastName,
                CreatedDate = comment.CreatedDate.ToString(),
                Content = comment.Content,
                CommentCount = _commentService.GetDefault(x => x.TweetID == tweetID && (x.Status == Core.Enum.Status.Active || x.Status == Core.Enum.Status.Updated)).Count(),
                LikeCount = _likeService.GetDefault(x => x.TweetID == tweetID && (x.Status == Core.Enum.Status.Active || x.Status == Core.Enum.Status.Updated)).Count()
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(Guid id)
        {
            Guid userID = _appUserService.FindByUserName(HttpContext.User.Identity.Name).ID;
            Comment comment = _commentService.GetByID(id);
            bool isDelete = false;


            if (comment.AppUserID == userID)
            {
                isDelete = true;
                _commentService.Remove(id);
                return Json(isDelete, JsonRequestBehavior.AllowGet);
            }
            else
            {
                isDelete = false;
                return Json(isDelete, JsonRequestBehavior.AllowGet);
            }
        }
    }
}