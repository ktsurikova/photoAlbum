using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Octokit;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            string usr = "";
            var authenticateResult = await HttpContext.GetOwinContext().Authentication.AuthenticateAsync("ExternalCookie");
            if (authenticateResult != null)
            {
                var tokenClaim = authenticateResult.Identity.Claims.FirstOrDefault(claim => claim.Type == "urn:token:github");
                if (tokenClaim != null)
                {
                    var accessToken = tokenClaim.Value;

                    var gitHubClient = new GitHubClient(new ProductHeaderValue("OAuthTestClient"));
                    gitHubClient.Credentials = new Credentials(accessToken);

                    var user = await gitHubClient.User.Current();

                    usr = accessToken;
                    usr += " name " + user.Name;
                    usr += " avatar link " + user.AvatarUrl;

                }
            }
            ViewBag.usr = usr;
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

        public ActionResult AuthorizeGitHub()
        {
            return new ChallengeResult("GitHub", "http://localhost:64636/Home/Index");
        }
    }
}