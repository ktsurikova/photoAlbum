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
            var filter2 = Builders<Photo>.Sort.Descending(p => p.UploadDate);
            return modelContext.Photos.Find(filter).Sort(filter2).Skip(skip).Limit(take).ToList()
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

        public IEnumerable<DalPhoto> GetByName(string name, int skip = 0, int take = 10)
        {
            return modelContext.Photos
                .Find(p => string.Compare(p.Name, name, StringComparison.InvariantCultureIgnoreCase) == 0)
                .Skip(skip).Limit(take).ToList().Select(p => p.ToDalPhoto());
        }

        public IEnumerable<DalPhoto> GetByUploadDate(DateTime dateTime, int skip = 0, int take = 10)
        {
            return modelContext.Photos.Find(p => p.UploadDate > dateTime).Skip(skip).Limit(take).ToList()
                .Select(p => p.ToDalPhoto());
        }

        public IEnumerable<DalPhoto> GetByUserId(int userId, int skip = 0, int take = 10)
        {
            var filter = Builders<Photo>.Sort.Descending(p => p.UploadDate);
            return modelContext.Photos.Find(p => p.UserId == userId).Sort(filter).Skip(skip).Limit(take).ToList()
                .Select(p => p.ToDalPhoto());
        }

        public IEnumerable<DalPhoto> GetSortedByNumberOfLikes(int skip = 0, int take = 10)
        {
            var filter = Builders<Photo>.Sort.Descending(p => p.NumberOfLikes);
            return modelContext.Photos.Find(p => true).Sort(filter).Skip(skip).Limit(take).ToList()
                .Select(p => p.ToDalPhoto());
        }

        public IEnumerable<DalPhoto> GetByUserLikes(int userId, int skip = 0, int take = 10)
        {
            var filter = Builders<Photo>.Filter.AnyEq(p => p.UserLikes, userId);
            return modelContext.Photos.Find(filter).Skip(skip).Limit(take).ToList()
                .Select(p => p.ToDalPhoto());
        }

        public void InsertMany(IEnumerable<DalPhoto> photos)
        {
            int i = 1;
            int id = GetId();
            foreach (var photo in photos)
            {
                photo.Id = id + i;
                i++;
            }
            modelContext.Photos.InsertMany(photos.Select(p => p.ToOrmPhoto()));
        }

        public void LikePhoto(int photoId, int userId)
        {
            //if (CheckIfLiked(photoId, userId))
            //    return;
            var updateLike = Builders<Photo>.Update.Inc(p => p.NumberOfLikes, 1);
            var updateUserLikes = Builders<Photo>.Update.AddToSet(p => p.UserLikes, userId);
            var combinedUpdateDefinition = Builders<Photo>.Update.Combine(updateLike, updateUserLikes);
            modelContext.Photos.UpdateOne(p => p.Id == photoId, combinedUpdateDefinition,
                new UpdateOptions() { IsUpsert = true });
        }

        public void DislikePhoto(int photoId, int userId)
        {
            //if (!CheckIfLiked(photoId, userId))
            //    return;
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

        public void ChangeName(int photoId, string newName)
        {
            var updateName = Builders<Photo>.Update.Set(p => p.Name, newName);
            modelContext.Photos.UpdateOne(p => p.Id == photoId, updateName,
                new UpdateOptions() { IsUpsert = true });
        }

        public void ChangeDescription(int photoId, string newDescription)
        {
            var updateDescription = Builders<Photo>.Update.Set(p => p.Description, newDescription);
            modelContext.Photos.UpdateOne(p => p.Id == photoId, updateDescription,
                new UpdateOptions() { IsUpsert = true });
        }

        public void AddTag(int photoId, string tag)
        {
            var updateTags = Builders<Photo>.Update.AddToSet(p => p.Tags, tag);
            modelContext.Photos.UpdateOne(p => p.Id == photoId, updateTags,
                new UpdateOptions() { IsUpsert = true });
        }

        public void DeleteAllPhotoOfUser(int userId)
        {
            modelContext.Photos.DeleteMany(p => p.UserId == userId);
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
