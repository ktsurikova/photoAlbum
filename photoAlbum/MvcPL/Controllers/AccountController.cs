using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Interface.Services;
using MvcPL.Models;
using MvcPL.Providers;

namespace MvcPL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpGet]
        public ActionResult Login(string returnUrl, bool? fancybox)
        {
            ViewBag.ReturnUrl = returnUrl;
            return PartialView("_SignIn");
        }

        [HttpGet]
        public ActionResult SingUp(string returnUrl, bool? fancybox)
        {
            return PartialView("_SingUp");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SingUp(SingUpViewModel model)
        {
            if (accountService.CheckIfUserExists(model.Login))
            {
                ModelState.AddModelError("", "User with this login already registered.");
                return PartialView("_SingUp", model);
            }

            if (ModelState.IsValid)
            {
                var membershipUser = ((CustomMembershipProvider)Membership.Provider)
                    .CreateUser(model.Login, model.Email, model.Password);

                if (membershipUser != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Login, false);
                    return RedirectToAction("Index", "Photos");
                }
                else
                {
                    ModelState.AddModelError("", "Error registration.");
                }
            }
            return PartialView("_SingUp", model);
        }

        public ActionResult ValidateLogin(string login)
        {
            if (accountService.CheckIfUserExists(login))
                return Json("login is already taken", JsonRequestBehavior.AllowGet);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}