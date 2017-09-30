using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public bool CheckIfUserExists(string login)
        {
            return userRepository.CheckIfUserExists(login);
        }

        public void CreateUser(BllUser user)
        {
            userRepository.Insert(user.ToDalUser());
        }

        public void EditeUserPtofile(int userId, string newName, byte[] newProfile)
        {
            if (!string.IsNullOrEmpty(newName))
            {
                userRepository.ChangeName(userId, newName);
            }
            if (newProfile!=null & newProfile.Length!=0)
            {
                userRepository.ChangeProfilePhoto(userId, newProfile);
            }
        }
    }
}
