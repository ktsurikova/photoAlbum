using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;
using DAL.Interface.DTO;

namespace BLL
{
    public static class BllEntityMapper
    {

        #region Photo
        public static BllPhoto ToBllPhoto(this DalPhoto photo)
        {
            return new BllPhoto()
            {
                Id = photo.Id,
                Name = photo.Name,
                Description = photo.Description,
                Image = photo.Image,
                NumberOfLikes = photo.NumberOfLikes,
                Tags = photo.Tags,
                UploadDate = photo.UploadDate,
                UserId = photo.UserId,
                UserLikes = photo.UserLikes
            };
        }

        public static DalPhoto ToDalPhoto(this BllPhoto photo)
        {
            return new DalPhoto()
            {
                Id = photo.Id,
                Name = photo.Name,
                Description = photo.Description,
                Image = photo.Image,
                NumberOfLikes = photo.NumberOfLikes,
                Tags = photo.Tags,
                UploadDate = photo.UploadDate,
                UserId = photo.UserId,
                UserLikes = photo.UserLikes
            };
        }
        #endregion


    }
}
