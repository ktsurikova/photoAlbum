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
        bool CheckIfUserExists(string login);
        void CreateUser(BllUser user);
    }
}
