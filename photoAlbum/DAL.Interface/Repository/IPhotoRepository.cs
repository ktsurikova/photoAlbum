using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;

namespace DAL.Interface.Repository
{
    public interface IPhotoRepository : IRepository<DalPhoto>
    {
        IEnumerable<DalPhoto> GetAll(int skip = 0, int take = 10);
        IEnumerable<DalPhoto> GetByTag(string tag, int skip = 0, int take = 10);
        int CountByTag(string tag);
        int CountByUserId(int userId);
        int CountAll();
        IEnumerable<DalPhoto> GetByUserId(int userId, int skip = 0, int take = 10);
        void LikePhoto(int photoId, int userId);
        void DislikePhoto(int photoId, int userId);
        bool CheckIfLiked(int photoId, int userId);
        IEnumerable<string> FindTag(string tag);
    }
}
