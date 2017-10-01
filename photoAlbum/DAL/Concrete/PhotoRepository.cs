using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using MongoDB.Driver;
using ORM;
using ORM.Entities;

namespace DAL.Concrete
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly ModelContext modelContext;

        public PhotoRepository()
        {
            modelContext = ModelContext.Create();
        }

        public DalPhoto GetById(int key)
        {
            return modelContext.Photos.Find(p => p.Id == key).FirstOrDefault().ToDalPhoto();
        }

        public void Insert(DalPhoto entity)
        {
            entity.Id = GetId() + 1;
            modelContext.Photos.InsertOne(entity.ToOrmPhoto());
        }

        private int GetId()
        {
            return modelContext.Photos.Aggregate().SortByDescending(p => p.Id).FirstOrDefault().Id;
        }

        public void Delete(DalPhoto entity)
        {
            modelContext.Photos.DeleteOne(p => p.Id == entity.Id);
        }

        public void Update(DalPhoto entity)
        {
            var filter = Builders<Photo>.Filter.Eq(u => u.Id, entity.Id);
            modelContext.Photos.ReplaceOne(filter, entity.ToOrmPhoto(), new UpdateOptions() { IsUpsert = true });
        }

        public IEnumerable<DalPhoto> GetAll(int skip = 0, int take = 10)
        {
            var filter = Builders<Photo>.Sort.Descending(p => p.UploadDate);
            return modelContext.Photos.Find(p => true).Sort(filter).Skip(skip).Limit(take).ToList()
                .Select(p => p.ToDalPhoto());
        }

        public IEnumerable<DalPhoto> GetByTag(string tag, int skip = 0, int take = 10)
        {
            var filter = Builders<Photo>.Filter.AnyEq(p => p.Tags, tag);
            return modelContext.Photos.Find(filter).Skip(skip).Limit(take).ToList()
                .Select(p => p.ToDalPhoto());
        }

        public int CountByTag(string tag)
        {
            var filter = Builders<Photo>.Filter.AnyEq(p => p.Tags, tag);
            return (int)modelContext.Photos.Find(filter).Count();
        }

        public int CountByUserId(int userId)
        {
            return (int)modelContext.Photos.Find(t => t.UserId == userId).Count();
        }

        public int CountAll()
        {
            return (int)modelContext.Photos.Find(t => true).Count();
        }

        public IEnumerable<DalPhoto> GetByUserId(int userId, int skip = 0, int take = 10)
        {
            var filter = Builders<Photo>.Sort.Descending(p => p.UploadDate);
            return modelContext.Photos.Find(p => p.UserId == userId).Sort(filter).Skip(skip).Limit(take).ToList()
                .Select(p => p.ToDalPhoto());
        }

        public void LikePhoto(int photoId, int userId)
        {
            var updateLike = Builders<Photo>.Update.Inc(p => p.NumberOfLikes, 1);
            var updateUserLikes = Builders<Photo>.Update.AddToSet(p => p.UserLikes, userId);
            var combinedUpdateDefinition = Builders<Photo>.Update.Combine(updateLike, updateUserLikes);
            modelContext.Photos.UpdateOne(p => p.Id == photoId, combinedUpdateDefinition,
                new UpdateOptions() { IsUpsert = true });
        }

        public void DislikePhoto(int photoId, int userId)
        {
            var updateLike = Builders<Photo>.Update.Inc(p => p.NumberOfLikes, -1);
            var updateUserLikes = Builders<Photo>.Update.Pull(p => p.UserLikes, userId);
            var combinedUpdateDefinition = Builders<Photo>.Update.Combine(updateLike, updateUserLikes);
            modelContext.Photos.UpdateOne(p => p.Id == photoId, combinedUpdateDefinition,
                new UpdateOptions() { IsUpsert = true });
        }

        public bool CheckIfLiked(int photoId, int userId)
        {
            var filter = Builders<Photo>.Filter.AnyEq(p => p.UserLikes, userId);
            var findPhoto = Builders<Photo>.Filter.Where(p => p.Id == photoId);
            return modelContext.Photos.Find(filter & findPhoto).FirstOrDefault() != null;
        }

        public IEnumerable<string> FindTag(string tag)
        {
            var s = modelContext.Photos.Find(p => p.Tags.Any(t => t.StartsWith(tag))).Limit(20).ToList()
                .Select(p => p.Tags);
            List<string> list = new List<string>();
            foreach (var arr in s)
            {
                foreach (var str in arr)
                {
                    if (str.StartsWith(tag) && (!list.Contains(str)))
                        list.Add(str);
                }
            }
            return list;
        }
    }
}
