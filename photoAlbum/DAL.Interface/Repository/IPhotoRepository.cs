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
        IEnumerable<DalPhoto> GetByName(string name, int skip = 0, int take = 10);
        IEnumerable<DalPhoto> GetByUploadDate(DateTime dateTime, int skip = 0, int take = 10);
        IEnumerable<DalPhoto> GetByUserId(int userId, int skip = 0, int take = 10);
        IEnumerable<DalPhoto> GetSortedByNumberOfLikes(int skip = 0, int take = 10);
        IEnumerable<DalPhoto> GetByUserLikes(int userId, int skip = 0, int take = 10);
        void InsertMany(IEnumerable<DalPhoto> photos);
        void LikePhoto(int photoId, int userId);
        void DislikePhoto(int photoId, int userId);
        bool CheckIfLiked(int photoId, int userId);
        //void ChangeName(int photoId, string newName);
        //void ChangeDescription(int photoId, string newDescription);
        void AddTag(int photoId, string tag);
        void DeleteAllPhotoOfUser(int userId);

        IEnumerable<string> FindTag(string tag);
    }
}
