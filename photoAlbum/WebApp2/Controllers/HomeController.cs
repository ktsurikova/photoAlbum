using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Util;
using Microsoft.Owin.Security;
using Microsoft.Owin.Host;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebApp2.Models;

namespace WebApp2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        public async Task<ActionResult> Secure(string url)
        {

            //var ss = HttpContext.GetOwinContext().Response;
            //var loginInfo = HttpContext.GetOwinContext().Authentication.AuthenticateAsync("Cookies");
            //string GoogleAccessCode = String.Empty;
            //if (loginInfo.Result.Identity.Claims.FirstOrDefault(c => c.Type.Equals("urn:tokens:google:accesstoken")) != null)
            //{
            //    var s = loginInfo.Result.Identity.Claims.FirstOrDefault(c => c.Type.Equals("urn:tokens:google:accesstoken"));
            //}


            var authenticateResult = await HttpContext.GetOwinContext().Authentication.AuthenticateAsync("Cookies");
            var a = authenticateResult.Identity.Claims.FirstOrDefault(claim => claim.Type == "urn:google:profile");
            //var aaa = authenticateResult.Identity.Claims.FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            //var aa = authenticateResult.Identity.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            //var b = authenticateResult.Identity.Claims.FirstOrDefault(claim => claim.Type == "picture");
            //var bb = authenticateResult.Identity.Claims.FirstOrDefault(claim => claim.Type == "gender");
            var ccDefault = authenticateResult.Identity.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email).Value;
            //var dd = authenticateResult.Identity.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name);


            Uri apiRequestUri = new Uri("http://picasaweb.google.com/data/entry/api/user/"+ ccDefault + "?alt=json");
            
            //dynamic userPicture;
            ////request profile image
            using (var webClient = new System.Net.WebClient())
            {
                try
                {
                    var json = webClient.DownloadString(apiRequestUri);
                    JObject o = JObject.Parse(json);
                    ViewBag.photo = o["entry"]["gphoto$thumbnail"]["$t"];
                }
                catch (Exception e)
                {

                }
                
                //userPicture = resul.picture;
            }
            //this.Session["userPicture"] = userPicture;

            //var accessToken = authenticateResult.Identity.Claims.Where(c => c.Type.Equals("urn:google:accesstoken")).FirstOrDefault();
            //Uri apiRequestUri = new Uri("https://www.googleapis.com/oauth2/v2/userinfo?access_token=" + accessToken);
            ////request profile image
            //using (var webClient = new System.Net.WebClient())
            //{
            //    var json = webClient.DownloadString(new Uri("https://www.googleapis.com/auth/plus.me"));
            //    dynamic result = JsonConvert.DeserializeObject(json);
            //    var userpicture = result.picture;
            //}

            //var authenticateResult = await HttpContext.GetOwinContext().Authentication.AuthenticateAsync("Cookies");
            //var authenticateResult2 = await HttpContext.GetOwinContext().Authentication.AuthenticateAsync("ExternalCookies");

            //var externalIdentity = await HttpContext.GetOwinContext().Authentication.AuthenticateAsync("Cookies");
            //var pictureClaim = externalIdentity.Identity.Claims.FirstOrDefault(c => c.Type.Equals("picture"));
            //var pictureUrl = pictureClaim.Value;

            //var accessToken = authenticateResult.Identity.Claims.Where(c => c.Type.Equals("urn:google:accesstoken")).Select(c => c.Value).FirstOrDefault();
            //Uri apiRequestUri = new Uri("https://www.googleapis.com/oauth2/v2/userinfo?access_token=" + accessToken);
            //dynamic userPicture;
            ////request profile image
            //using (var webClient = new System.Net.WebClient())
            //{
            //    var json = webClient.DownloadString(apiRequestUri);
            //    dynamic resul = JsonConvert.DeserializeObject(json);
            //    userPicture = resul.picture;
            //}
            //this.Session["userPicture"] = userPicture;
            //get access token to use in profile image request

            return View();
        }

        public ActionResult Login(string returnUrl)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, "christinedaae4@gmail.com"));
            claims.Add(new Claim(ClaimTypes.Name, "Christine"));

            HttpContext.GetOwinContext().Authentication.SignIn(
                new ClaimsIdentity(claims, "Cookies"));

            return new RedirectResult(returnUrl);

            //return new ChallengeResult("Google",
            //    Url.Action("ExternalLoginCallback", "Home", new { ReturnUrl = returnUrl }));

        }

        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            return new RedirectResult(returnUrl);
        }

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return Redirect("/");
        }

        //public async Task<UserCredential> GetCredentialForApiAsync()
        //{
        //    var initializer = new GoogleAuthorizationCodeFlow.Initializer
        //    {
        //        ClientSecrets = new ClientSecrets
        //        {
        //            ClientId = "147055946353-lhpck1prplphhlae48vig6nq96cp4acm.apps.googleusercontent.com",
        //            ClientSecret = "InXJjZhf_n5hPDE18sHoPp1t",
        //        },
        //        Scopes = new[] {
        //            "https://www.googleapis.com/auth/plus.login",
        //            "https://www.googleapis.com/auth/plus.me",
        //            "https://www.googleapis.com/auth/userinfo.email" }
        //    };
        //    var flow = new GoogleAuthorizationCodeFlow(initializer);

        //    var identity = await HttpContext.GetOwinContext().Authentication.AuthenticateAsync("Cookies");

        //    var userId = identity.Identity.FindFirst("GoogleUserId").Value;

        //    var token = new TokenResponse()
        //    {
        //        AccessToken = identity.Identity.FindFirst("Google_AccessToken").Value,
        //        RefreshToken = identity.Identity.FindFirst("GoogleRefreshToken").Value,
        //        Issued = DateTime.FromBinary(long.Parse(identity.Identity.FindFirst("GoogleTokenIssuedAt").Value)),
        //        ExpiresInSeconds = long.Parse(identity.Identity.FindFirst("GoogleTokenExpiresIn").Value),
        //    };

        //    return new UserCredential(flow, userId, token);
        //}
    }
}