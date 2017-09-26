using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using BLL.Interface.Services;

namespace MvcPL.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        public IAccountService AccountService
            => (IAccountService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IAccountService));

        public override bool IsUserInRole(string username, string roleName)
        {
            IEnumerable<string> roles = AccountService.GetUserByLogin(username).Roles;
            foreach (var role in roles)
            {
                if (string.Compare(role, roleName, StringComparison.InvariantCultureIgnoreCase) == 0)
                    return true;
            }
            return false;
        }

        public override string[] GetRolesForUser(string username)
        {
            IEnumerable<string> roles = AccountService.GetUserByLogin(username).Roles;
            string[] arrRoles = new string[roles.Count()];
            int i = 0;
            foreach (var role in roles)
            {
                arrRoles[i] = role;
                i++;
            }
            return arrRoles;
        }



        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}