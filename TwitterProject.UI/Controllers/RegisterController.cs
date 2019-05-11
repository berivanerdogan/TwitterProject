using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TwitterProject.Model.Option;
using TwitterProject.Service.Option;
using TwitterProject.UI.Models.VM;

namespace TwitterProject.UI.Controllers
{
    public class RegisterController : Controller
    {
        AppUserService _appUserService;
        public RegisterController()
        {
            _appUserService = new AppUserService();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(AppUser user)
        {
            _appUserService.Add(user);
            return Redirect("/Account/Login");
        }
    }
}