using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Interface.Services;
using Microsoft.Owin.Security;
using MvcPL.Models;
using MvcPL.Models.auth;
using MvcPL.Providers;
using Newtonsoft.Json.Linq;

namespace MvcPL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        [HttpGet]
        public ActionResult SignIn(string returnUrl, bool? fancybox)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (fancybox == true)
                return PartialView("_SignIn");
            return View("SignIn");
        }

        [HttpPost]
        public ActionResult SignIn(SignInViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (accountService.ValidateUser(viewModel.Login, viewModel.Password))
                {
                    SetCookies(viewModel.Login, viewModel.Login, accountService.GetUserByLogin(viewModel.Login).Email, "forms");

                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Photos");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login or password.");
                }
            }
            return View("SignIn", viewModel);
        }

        [HttpGet]
        public ActionResult SignInGoogle(string returnUrl)
        {
            string url = Url.IsLocalUrl(returnUrl) ? returnUrl : Url.Action("Index", "Photos");

            return new ChallengeResult("Google",
                Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = url }));
        }

        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            string userEmail = null;
            try
            {
                userEmail = User.Identity.GetEmail();
            }
            catch (ArgumentNullException)
            {

            }

            if (userEmail == null)
            {
                return View("SignIn", returnUrl);
            }

            if (!accountService.CheckIfUserExistsByEmail(userEmail))
            {
                AuthenticationManager.SignOut();
                TempData["error"] = "Incorrect login or password.";
                return new RedirectResult(Url.Action("SignIn", "Account", returnUrl));
            }

            SetCookies(userEmail.Split('@')[0], "google");

            //User.Identity.AddClaims(ClaimTypes.AuthenticationMethod, "google");
            //User.Identity.AddClaims("Login", userEmail.Split('@')[0]);

            return new RedirectResult(returnUrl);
        }

        public ActionResult ExternalSignUpCallback(string returnUrl)
        {
            string userEmail = null;
            try
            {
                userEmail = User.Identity.GetEmail();
            }
            catch (ArgumentNullException)
            {

            }

            if (userEmail == null)
            {
                return View("SignUp");
            }

            if (accountService.CheckIfUserExistsByEmail(userEmail))
            {
                TempData["error"] = $"user {userEmail} alreade exists";
                AuthenticationManager.SignOut();
                return new RedirectResult(Url.Action("SignUp", "Account"));
            }

            IIdentity identity = User.Identity;
            var email = identity.GetEmail();
            var name = identity.GetClaims(ClaimTypes.GivenName);
            string photoUrl = null;
            //identity.AddClaims(ClaimTypes.AuthenticationMethod, "google");
            var login = userEmail.Split('@')[0];
            //User.Identity.AddClaims("Login", login);

            SetCookies(login, "google");

            Uri apiRequestUri = new Uri("http://picasaweb.google.com/data/entry/api/user/" + email + "?alt=json");

            using (var webClient = new System.Net.WebClient())
            {
                try
                {
                    var json = webClient.DownloadString(apiRequestUri);
                    JObject o = JObject.Parse(json);
                    photoUrl = o["entry"]["gphoto$thumbnail"]["$t"].Value<string>();
                }
                catch (Exception)
                { 

                }
            }

            accountService.CreateUser(login, email, name, photoUrl, "google");

            return RedirectToAction("Index", "Photos");
        }

        [HttpGet]
        public ActionResult SignUp(string returnUrl, bool? fancybox)
        {
            if (fancybox == true)
                return PartialView("_SignUp");
            return View("SignUp");
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult SignUp(SignUpViewModel model)
        {
            if (accountService.CheckIfUserExists(model.Login))
            {
                ModelState.AddModelError("", "User with this login already registered.");
                return View("SignUp", model);
            }

            if (ModelState.IsValid)
            {
                var user = accountService.CreateUser(model.Login, model.Email, model.Password);

                if (user != null)
                {
                    SetCookies(user.Login, user.Login, user.Email, "forms");
                    return RedirectToAction("Index", "Photos");
                }
                else
                {
                    ModelState.AddModelError("", "Error registration.");
                }
            }
            return View("SignUp", model);
        }

        public ActionResult SignUpGoogle()
        {
            return new ChallengeResult("Google",
                Url.Action("ExternalSignUpCallback", "Account"));
        }

        public ActionResult ValidateSignUp(string login)
        {
            if (accountService.CheckIfUserExists(login))
                return Json("login is already taken", JsonRequestBehavior.AllowGet);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ValidateSignIn(string login)
        {
            if (!accountService.CheckIfUserExists(login))
                return Json("there's no users with such login", JsonRequestBehavior.AllowGet);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult SignOut()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Photos");
        }

        [NonAction]
        private void SetCookies(string login, string nameIdentifier, string email, string method)
        {
            var claims = new List<Claim>
            {
                new Claim("Login", login),
                new Claim(ClaimTypes.NameIdentifier, nameIdentifier),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.AuthenticationMethod, method)
            };

            AuthenticationManager.SignOut();
            AuthenticationManager.SignIn(new AuthenticationProperties()
            {
                IsPersistent = false
            }, new ClaimsIdentity(claims, "Cookies"));
        }

        [NonAction]
        private void SetCookies(string login, string method)
        {
            User.Identity.AddClaims(ClaimTypes.AuthenticationMethod, method);
            User.Identity.AddClaims("Login", login);

            AuthenticationManager.AuthenticationResponseGrant = new AuthenticationResponseGrant(
                new ClaimsPrincipal(User.Identity), new AuthenticationProperties() { IsPersistent = false} );
        }
    }
}