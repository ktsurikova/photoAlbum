using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;

[assembly: OwinStartup(typeof(MvcPL.Startup))]

namespace MvcPL
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var cookieOptions = new CookieAuthenticationOptions
            {
                LoginPath = new PathString("/Account/SignIn"),
                Provider = new CookieAuthenticationProvider(),
                AuthenticationType = "Cookies"
            };

            app.UseCookieAuthentication(cookieOptions);

            app.SetDefaultSignInAsAuthenticationType("Cookies");

            var googleOAuth2AuthenticationOptions = new GoogleOAuth2AuthenticationOptions
            {
                ClientId = "147055946353-lhpck1prplphhlae48vig6nq96cp4acm.apps.googleusercontent.com",
                ClientSecret = "InXJjZhf_n5hPDE18sHoPp1t",
                Provider = new GoogleOAuth2AuthenticationProvider()
            };

            app.UseGoogleAuthentication(googleOAuth2AuthenticationOptions);
        }
    }
}
