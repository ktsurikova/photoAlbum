using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;
using ORM.Entities;

namespace DAL
{
    public static class DalEntityMapper
    {
        #region User
        public static DalUser ToDalUser(this User user)
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

        public static User ToOrmUser(this DalUser user)
        {
            return new User()
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
        public static DalPhoto ToDalPhoto(this Photo photo)
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

        public static Photo ToOrmPhoto(this DalPhoto photo)
        {
            return new Photo()
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
        public static DalAuthor ToDalAuthor(this Author author)
        {
            return new DalAuthor()
            {
                Id = author.Id,
                Name = author.Name
            };
        }

        public static Author ToOrmAuthor(this DalAuthor author)
        {
            return new Author()
            {
                Id = author.Id,
                Name = author.Name
            };
        }
        #endregion

        #region Comment
        public static DalComment ToDalComment(this Comment comment)
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

        public static Comment ToOrmComment(this DalComment comment)
        {
            return new Comment()
            {
                Id = comment.Id,
                Author = comment.Author.ToOrmAuthor(),
                PhotoId = comment.PhotoId,
                Posted = comment.Posted,
                Text = comment.Text
            };
        } 
        #endregion
    }
}
