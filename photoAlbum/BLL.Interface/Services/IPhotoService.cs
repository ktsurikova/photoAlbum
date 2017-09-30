using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface IPhotoService
    {
        void Add(BllPhoto photo);
        IEnumerable<string> FindTags(string tag);
        int CountByTag(string tag);
        IEnumerable<BllPhoto> GetByTag(string tag, int skip, int take);
        IEnumerable<BllPhoto> GetAll(int skip, int take);
        BllPhoto GetById(int id);
        IEnumerable<BllPhoto> GetByUserId(int userId, int skip, int take);
        int CountByUserId(int id);
        void LikePhoto(int userId, int photoId);
        void DislikePhoto(int userId, int photoId);
        void AddComment(BllComment comment);
        int CountCommentByPhotoId(int photoId);
        IEnumerable<BllComment> GetCommentsByPhotoId(int photoId, int skip, int take);
        void Delete(int photoId);
    }
}
