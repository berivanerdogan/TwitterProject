using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TwitterProject.UI.Areas.Member.Controllers
{
    public class TweetController : Controller
    {
        // GET: Member/Tweet
        public ActionResult Index()
        {
            return View();
        }
    }
}