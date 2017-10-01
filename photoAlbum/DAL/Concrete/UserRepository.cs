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

        public void ChangeName(int userId, string newName)
        {
            var updateName = Builders<User>.Update.Set(p => p.Name, newName);
            modelContext.Users.UpdateOne(p => p.Id == userId, updateName,
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

        public void ChangeProfilePhoto(int userId, byte[] newProfilePhoto)
        {
            var updatePhoto = Builders<User>.Update.Set(p => p.ProfilePhoto, newProfilePhoto);
            modelContext.Users.UpdateOne(p => p.Id == userId, updatePhoto,
                new UpdateOptions() { IsUpsert = true });
        }
    }
}
