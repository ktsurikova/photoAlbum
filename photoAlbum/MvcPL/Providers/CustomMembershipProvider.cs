using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Security;
using BLL.Interface.Entities;
using BLL.Interface.Services;

namespace MvcPL.Providers
{
    public class CustomMembershipProvider : MembershipProvider
    {
        public IAccountService AccountService =>
            (IAccountService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IAccountService));

        public MembershipUser CreateUser(string login, string email, string password)
        {
            MembershipUser membershipUser = GetUser(email, false);

            if (membershipUser != null)
            {
                return null;
            }

            var user = new BllUser()
            {
                Login = login,
                Email = email,
                Password = Crypto.HashPassword(password)
            };

            var s = new List<string>(1) {"User"};
            user.Roles = s;

            AccountService.CreateUser(user);
            membershipUser = GetUser(login, false);
            return membershipUser;
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            if (!AccountService.CheckIfUserExists(username))
                return null;

            var user = AccountService.GetUserByLogin(username);

            var memberUser = new MembershipUser("CustomMembershipProvider", user.Login,
                null, user.Email, null, null,
                false, false, DateTime.MinValue , 
                DateTime.MinValue, DateTime.MinValue,
                DateTime.MinValue, DateTime.MinValue);

            return memberUser;
        }

        public override bool ValidateUser(string username, string password)
        {
            if (!AccountService.CheckIfUserExists(username))
                return false;
            var user = AccountService.GetUserByLogin(username);
            return user != null && Crypto.VerifyHashedPassword(user.Password, password);
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }








        #region stub

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion,
            string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion,
            string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordRetrieval { get; }
        public override bool EnablePasswordReset { get; }
        public override bool RequiresQuestionAndAnswer { get; }
        public override string ApplicationName { get; set; }
        public override int MaxInvalidPasswordAttempts { get; }
        public override int PasswordAttemptWindow { get; }
        public override bool RequiresUniqueEmail { get; }
        public override MembershipPasswordFormat PasswordFormat { get; }
        public override int MinRequiredPasswordLength { get; }
        public override int MinRequiredNonAlphanumericCharacters { get; }
        public override string PasswordStrengthRegularExpression { get; }
    } 
    #endregion
}