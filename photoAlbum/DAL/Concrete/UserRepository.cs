using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using MongoDB.Driver;
using ORM;
using ORM.Entities;

namespace DAL.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly ModelContext modelContext;

        public UserRepository()
        {
            modelContext = ModelContext.Create();
        }

        //public IEnumerable<DalUser> GetAll()
        //{
        //    var filter = Builders<User>.Filter.Empty;
        //    return modelContext.Users.FindSync<User>(filter).ToList().Select(u => u.ToDalUser());
        //}

        public DalUser GetById(int key)
        {
            return modelContext.Users.Find(u => u.Id == key).FirstOrDefault().ToDalUser();
        }

        public void Insert(DalUser entity)
        {
            entity.Id = GetId() + 1;
            modelContext.Users.InsertOne(entity.ToOrmUser());
        }

        private int GetId()
        {
            return modelContext.Users.Aggregate().SortByDescending(p => p.Id).FirstOrDefault().Id;
        }

        public void Delete(DalUser entity)
        {
            modelContext.Users.DeleteOne(i => i.Id == entity.Id);
        }

        public void Update(DalUser entity)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, entity.Id);
            modelContext.Users.ReplaceOne(filter, entity.ToOrmUser(), new UpdateOptions() { IsUpsert = true });
        }

        //public void ChangeLogin(int userId, string newLogin)
        //{
        //    var updateLogin = Builders<User>.Update.Set(p => p.Login, newLogin);
        //    modelContext.Users.UpdateOne(p => p.Id == userId, updateLogin,
        //        new UpdateOptions() { IsUpsert = true });
        //}

        public void ChangeName(int userId, string newName)
        {
            var updateName = Builders<User>.Update.Set(p => p.Name, newName);
            modelContext.Users.UpdateOne(p => p.Id == userId, updateName,
                new UpdateOptions() { IsUpsert = true });
        }

        //public void ChangeEmail(int userId, string newEmail)
        //{
        //    var updateEmail = Builders<User>.Update.Set(p => p.Email, newEmail);
        //    modelContext.Users.UpdateOne(p => p.Id == userId, updateEmail,
        //        new UpdateOptions() { IsUpsert = true });
        //}

        public void ChangePassword(int userId, string newPassword)
        {
            var updatePassword = Builders<User>.Update.Set(p => p.Password, newPassword);
            modelContext.Users.UpdateOne(p => p.Id == userId, updatePassword,
                new UpdateOptions() { IsUpsert = true });
        }

        public DalUser GetUserByLogin(string login)
        {
            return modelContext.Users.Find(u => u.Login == login).FirstOrDefault().ToDalUser();
        }

        public bool CheckIfUserExists(string login)
        {
            if (modelContext.Users.Find(u => u.Login == login).FirstOrDefault() == null)
                return false;
            return true;
        }

        //public void ChangePhone(int userId, string newPhone)
        //{
        //    var updatePhone = Builders<User>.Update.Set(p => p.Phone, newPhone);
        //    modelContext.Users.UpdateOne(p => p.Id == userId, updatePhone,
        //        new UpdateOptions() { IsUpsert = true });
        //}

        public void ChangeProfilePhoto(int userId, byte[] newProfilePhoto)
        {
            var updatePhoto = Builders<User>.Update.Set(p => p.ProfilePhoto, newProfilePhoto);
            modelContext.Users.UpdateOne(p => p.Id == userId, updatePhoto,
                new UpdateOptions() { IsUpsert = true });
        }
    }
}
