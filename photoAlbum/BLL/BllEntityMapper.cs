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

        #region User
        public static DalUser ToDalUser(this BllUser user)
        {
            return new DalUser()
            {
                Id = user.Id,
                Email = user.Email,
                Login = user.Login,
                Name = user.Name,
                Password = user.Password,
                Phone = user.Phone,
                ProfilePhoto = user.ProfilePhoto,
                Roles = user.Roles
            };
        }

        public static BllUser ToBllUser(this DalUser user)
        {
            return new BllUser()
            {
                Id = user.Id,
                Email = user.Email,
                Login = user.Login,
                Name = user.Name,
                Password = user.Password,
                Phone = user.Phone,
                ProfilePhoto = user.ProfilePhoto,
                Roles = user.Roles
            };
        }
        #endregion


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

        #region Author
        public static DalAuthor ToDalAuthor(this BllAuthor author)
        {
            return new DalAuthor()
            {
                Id = author.Id,
                Name = author.Name
            };
        }

        public static BllAuthor ToBllAuthor(this DalAuthor author)
        {
            return new BllAuthor()
            {
                Id = author.Id,
                Name = author.Name
            };
        }
        #endregion

        #region Comment
        public static DalComment ToDalComment(this BllComment comment)
        {
            return new DalComment()
            {
                Id = comment.Id,
                Author = comment.Author.ToDalAuthor(),
                PhotoId = comment.PhotoId,
                Posted = comment.Posted,
                Text = comment.Text
            };
        }

        public static BllComment ToBllComment(this DalComment comment)
        {
            return new BllComment()
            {
                Id = comment.Id,
                Author = comment.Author.ToBllAuthor(),
                PhotoId = comment.PhotoId,
                Posted = comment.Posted,
                Text = comment.Text
            };
        }
        #endregion

    }
}
