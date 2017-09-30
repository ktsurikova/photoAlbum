using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;

namespace DAL.Interface.Repository
{
    public interface ICommentRepository : IRepository<DalComment>
    {
        IEnumerable<DalComment> GetByPhotoId(int photoId, int skip = 0, int take = 10);
        void DeleteAllCommentsToPhoto(int photoId);
        void DeleteAllCommentsOfUser(int userId);
        int CountByPhotoId(int photoId);
    }
}
