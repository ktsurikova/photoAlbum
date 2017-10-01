using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;

namespace DAL.Interface.Repository
{
    public interface IUserRepository : IRepository<DalUser>
    {
        void ChangeName(int userId, string newName);
        DalUser GetUserByLogin(string login);
        bool CheckIfUserExists(string login);
        void ChangeProfilePhoto(int userId, byte[] newProfilePhoto);
    }
}
