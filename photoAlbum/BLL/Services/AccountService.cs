using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using DAL.Interface.Repository;

namespace BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository userRepository;

        public AccountService(IUserRepository repository)
        {
            userRepository = repository;
        }

        public BllUser GetUserByLogin(string login)
        {
            return userRepository.GetUserByLogin(login).ToBllUser();
        }

        public BllUser GetUserById(int id)
        {
            return userRepository.GetById(id).ToBllUser();
        }

        public BllUser GetUserByEmail(string email)
        {
            return userRepository.GetUserByEmail(email).ToBllUser();
        }

        public bool CheckIfUserExists(string login)
        {
            return userRepository.CheckIfUserExists(login);
        }

        public bool CheckIfUserExistsByEmail(string email)
        {
            return userRepository.CheckIfUserExistsByEmail(email);
        }

        public void CreateUser(BllUser user)
        {
            userRepository.Insert(user.ToDalUser());
        }

        public void CreateUser(string login, string email, string name, string photoUrl, string type)
        {
            var user = new BllUser()
            {
                Email = email,
                Name = name,
                Login = login
            };
            if (photoUrl != null)
            {
                user.ProfilePhoto = new BllProfilePhoto()
                {
                    Type = type,
                    Url = photoUrl
                };
            }

            var s = new List<string>(1) { "User" };
            user.Roles = s;

            CreateUser(user);
        }

        public BllUser CreateUser(string login, string email, string password)
        {
            var user = new BllUser()
            {
                Login = login,
                Email = email,
                Password = Crypto.HashPassword(password)
            };

            var s = new List<string>(1) { "User" };
            user.Roles = s;

            CreateUser(user);

            return userRepository.GetUserByLogin(login).ToBllUser();
        }

        public void EditeUserPtofile(int userId, string newName, byte[] newProfile)
        {
            if (!string.IsNullOrEmpty(newName))
            {
                userRepository.ChangeName(userId, newName);
            }
            if (newProfile != null & newProfile.Length != 0)
            {
                //userRepository.ChangeProfilePhoto(userId, newProfile);
            }
        }

        public bool ValidateUser(string username, string password)
        {
            if (!CheckIfUserExists(username))
                return false;
            var user = GetUserByLogin(username);
            return user != null && Crypto.VerifyHashedPassword(user.Password, password);
        }
    }
}
