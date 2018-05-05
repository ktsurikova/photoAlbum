using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace MvcPL.Models.auth
{
    public static class IdentityExtensions
    {
        public static T GetUserId<T>(this IIdentity identity) where T : IConvertible
        {
            if (identity == null)
            {
                throw new ArgumentNullException(nameof(identity));
            }
            var ci = identity as ClaimsIdentity;
            var id = ci?.FindFirst(ClaimTypes.NameIdentifier);
            if (id != null)
            {
                return (T)Convert.ChangeType(id.Value, typeof(T), CultureInfo.InvariantCulture);
            }
            return default(T);
        }
        public static string GetUserRole(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException(nameof(identity));
            }
            var ci = identity as ClaimsIdentity;
            string role = "";
            var id = ci?.FindFirst(ClaimsIdentity.DefaultRoleClaimType);
            if (id != null)
                role = id.Value;
            return role;
        }

        public static string GetEmail(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException(nameof(identity));
            }
            var ci = identity as ClaimsIdentity;
            string email = "";
            var id = ci?.FindFirst(ClaimTypes.Email);
            if (id != null)
                email = id.Value;
            return email;
        }

        public static string GetClaims(this IIdentity identity, string claimsType)
        {
            if (identity == null)
            {
                throw new ArgumentNullException(nameof(identity));
            }
            var ci = identity as ClaimsIdentity;
            string method = "";
            var id = ci?.FindFirst(claimsType);
            if (id != null)
                method = id.Value;
            return method;
        }

        public static bool AddClaims(this IIdentity identity, string claimsType, string value)
        {
            if (identity == null)
            {
                throw new ArgumentNullException(nameof(identity));
            }
            var ci = identity as ClaimsIdentity;
            string method = "";
            var id = ci?.FindFirst(claimsType);
            if (id != null || ci == null)
                return false;
            ci.AddClaim(new Claim(claimsType, value));
            return true;
        }
    }
}