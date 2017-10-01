using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using MongoDB.Driver;
using ORM;
using ORM.Entities;

namespace DAL.Concrete
{
    public class CommentRepository : ICommentRepository
    {

        private readonly ModelContext modelContext;

        public CommentRepository()
        {
            modelContext = ModelContext.Create();
        }

        public DalComment GetById(int key)
        {
            return modelContext.Comments.Find(c => c.Id == key).FirstOrDefault().ToDalComment();
        }

        public void Insert(DalComment entity)
        {
            entity.Id = GetId() + 1;
            modelContext.Comments.InsertOne(entity.ToOrmComment());
        }

        private int GetId()
        {
            return modelContext.Comments.Aggregate().SortByDescending(p => p.Id).FirstOrDefault().Id;
        }

        public void Delete(DalComment entity)
        {
            modelContext.Comments.DeleteOne(p => p.Id == entity.Id);
        }

        public void Update(DalComment entity)
        {
            var filter = Builders<Comment>.Filter.Eq(u => u.Id, entity.Id);
            modelContext.Comments.ReplaceOne(filter, entity.ToOrmComment(), new UpdateOptions() { IsUpsert = true });
        }

        public IEnumerable<DalComment> GetByPhotoId(int photoId, int skip = 0, int take = 10)
        {
            return modelContext.Comments.Find(c => c.PhotoId == photoId).SortByDescending(p=>p.Posted).Skip(skip).Limit(take).ToList()
                .Select(c => c.ToDalComment());
        }

        public void DeleteAllCommentsToPhoto(int photoId)
        {
            modelContext.Comments.DeleteMany(c => c.PhotoId == photoId);
        }

        public int CountByPhotoId(int photoId)
        {
            return (int)modelContext.Comments.Find(t => t.PhotoId == photoId).Count();
        }
    }
}
