using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TwitterProject.Model.Option;
using TwitterProject.Service.Option;
using TwitterProject.UI.Models.VM;

namespace TwitterProject.UI.Controllers
{
    public class AccountController : Controller
    {
        AppUserService _appUserService;
        public AccountController()
        {
            _appUserService = new AppUserService();
        }
        public ActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                AppUser appUser = _appUserService.FindByUserName(User.Identity.Name);

                Session["FullName"] = appUser.FirstName + " " + appUser.LastName;
                Session["UserImage"] = appUser.UserImage;

                return Redirect("/Member/Home/MemberHomeIndex");
            }

            TempData["class"] = "custom-hide";
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginVM credential)
        {
            if (ModelState.IsValid)
            {
                if (_appUserService.CheckCredentials(credential.UserName,credential.Password))
                {
                    AppUser user = _appUserService.FindByUserName(credential.UserName);
                    string cookie = user.UserName;
                    FormsAuthentication.SetAuthCookie(cookie, true);
                    Session["FullName"] = user.FirstName + " " + user.LastName;
                    Session["UserImage"] = user.UserImage;

                    return Redirect("/Member/Home/MemberHomeIndex");
                }
                else
                {
                    ViewData["error"] = "Username or password is wrong";
                    return View();
                }
            }
            TempData["class"] = "custom-show";
            return View();
        }
        public ActionResult LoginOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("/Account/Login");
        }
    }
}