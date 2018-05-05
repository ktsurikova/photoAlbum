using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface IAccountService
    {
        BllUser GetUserByLogin(string login);
        BllUser GetUserById(int id);
        BllUser GetUserByEmail(string email);
        bool CheckIfUserExists(string login);
        bool CheckIfUserExistsByEmail(string email);
        void CreateUser(BllUser user);
        void CreateUser(string login, string email, string name, string photoUrl, string type);
        BllUser CreateUser(string login, string email, string password);
        void EditeUserPtofile(int userId, string newName, byte[] newProfile);
        bool ValidateUser(string username, string password);
    }
}
