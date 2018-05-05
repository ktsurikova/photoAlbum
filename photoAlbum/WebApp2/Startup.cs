using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.Twitter;
using Owin;

[assembly: OwinStartup(typeof(WebApp2.Startup))]
namespace WebApp2
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var cookieOptions = new CookieAuthenticationOptions
            {
                LoginPath = new PathString("/Home/Login"),
                Provider = new CookieAuthenticationProvider()
            };

            app.UseCookieAuthentication(cookieOptions);

            app.SetDefaultSignInAsAuthenticationType("Cookies");

            var authenticationOptions = new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "147055946353-lhpck1prplphhlae48vig6nq96cp4acm.apps.googleusercontent.com",
                ClientSecret = "InXJjZhf_n5hPDE18sHoPp1t"
            };

            //authenticationOptions.Scope.Add("https://www.googleapis.com/auth/plus.login");
            //authenticationOptions.Scope.Add("https://www.googleapis.com/auth/plus.me");
            //authenticationOptions.Scope.Add("https://www.googleapis.com/auth/userinfo.email");           
            //authenticationOptions.Scope.Add("https://www.googleapis.com/auth/userinfo.profile");
            //authenticationOptions.Scope.Add("https://www.googleapis.com/auth/user.phonenumbers.read");


            //authenticationOptions.Provider = new GoogleOAuth2AuthenticationProvider()
            //{
            //    OnAuthenticated = (context) =>
            //        {
            //            context.Identity.AddClaim(new Claim("urn:google:name", context.Identity.FindFirst(ClaimTypes.Name).Value));
            //            context.Identity.AddClaim(new Claim("urn:google:email", context.Identity.FindFirst(ClaimTypes.Email).Value));
            //            //This following line is need to retrieve the profile image
            //            context.Identity.AddClaim(new Claim("urn:google:accesstoken", context.AccessToken, ClaimValueTypes.String, "Google"));

            //            return Task.FromResult(0);
            //        }

            //};

            var googleOAuth2AuthenticationOptions = new GoogleOAuth2AuthenticationOptions
            {
                ClientId = "147055946353-lhpck1prplphhlae48vig6nq96cp4acm.apps.googleusercontent.com",
                ClientSecret = "InXJjZhf_n5hPDE18sHoPp1t",
                //AccessType = "offline",
                //SignInAsAuthenticationType = "ExternalCookie",
                //CallbackPath = new PathString("/Account/Login"),
                Provider = new GoogleOAuth2AuthenticationProvider()
                {
                    //OnApplyRedirect = context =>
                    //{
                    //    string redirect = context.RedirectUri;
                    //    redirect += "&access_type=offline";
                    //    redirect += "&approval_prompt=force";
                    //    redirect += "&include_granted_scopes=true";


                    //    context.Response.Redirect(redirect);

                    //},
                    //OnAuthenticated = context =>
                    //{
                    //    TimeSpan expiryDuration = context.ExpiresIn ?? new TimeSpan();
                    //    context.Identity.AddClaim(new Claim("urn:tokens:google:email", context.Email));
                    //    context.Identity.AddClaim(new Claim("urn:tokens:google:url", context.GivenName));
                    //    if (!String.IsNullOrEmpty(context.RefreshToken))
                    //    {
                    //        context.Identity.AddClaim(new Claim("urn:tokens:google:refreshtoken", context.RefreshToken));
                    //    }
                    //    context.Identity.AddClaim(new Claim("urn:tokens:google:accesstoken", context.AccessToken));
                    //    if (context.User.GetValue("hd") != null)
                    //    {

                    //        context.Identity.AddClaim(new Claim("urn:tokens:google:hd", context.User.GetValue("hd").ToString()));
                    //    }
                    //    context.Identity.AddClaim(new Claim("urn:tokens:google:accesstokenexpiry", DateTime.UtcNow.Add(expiryDuration).ToString()));

                    //    return System.Threading.Tasks.Task.FromResult<object>(null);
                    //},
                    //OnReturnEndpoint = context =>
                    //{
                    //    var identity = (ClaimsIdentity)context.Identity;
                    //    var profileImg = context.OwinContext.Response.Cookies;
                    //    //User["image"].Value<string>("url");
                    //    //identity.AddClaim(new Claim("profileImg", profileImg));
                    //    return Task.FromResult(0);
                    //}
                }

            };

            //googleOAuth2AuthenticationOptions.Scope.Add("openid");
            //googleOAuth2AuthenticationOptions.Scope.Add("email");
            ////googleOAuth2AuthenticationOptions.Scope.Add("https://www.googleapis.com/auth/plus.login");
            ////googleOAuth2AuthenticationOptions.Scope.Add("https://www.googleapis.com/auth/plus.me");
            ////googleOAuth2AuthenticationOptions.Scope.Add("https://www.googleapis.com/auth/userinfo.email");
            ////googleOAuth2AuthenticationOptions.Scope.Add("email");

            app.UseGoogleAuthentication(googleOAuth2AuthenticationOptions);
            //app.UseGoogleAuthentication(authenticationOptions);
        }
    }
}