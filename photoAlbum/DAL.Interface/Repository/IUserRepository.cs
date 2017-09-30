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
        //void ChangeLogin(int userId, string newLogin);
        void ChangeName(int userId, string newName);
        //void ChangeEmail(int userId, string newEmail);
        void ChangePassword(int userId, string newPassword);
        DalUser GetUserByLogin(string login);
        bool CheckIfUserExists(string login);
        //void ChangePhone(int userId, string newPhone);
        void ChangeProfilePhoto(int userId, byte[] newProfilePhoto);
    }
}
