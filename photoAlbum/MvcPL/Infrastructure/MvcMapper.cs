using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using BLL.Interface.Entities;
using MvcPL.Models;

namespace MvcPL.Infrastructure
{
    public static class MvcMapper
    {
        public static PhotoViewModel ToPhotoViewModel(this BllPhoto photo)
        {
            return new PhotoViewModel()
            {
                Id = photo.Id,
                //Name = photo.Name,
                //Description = photo.Description,
                Image = photo.Image,
                //NumberOfLikes = photo.NumberOfLikes,
                //Tags = photo.Tags,
                //UploadDate = photo.UploadDate,
                //UserId = photo.UserId,
                //UserLikes = photo.UserLikes
            };
        }

        public static PhotoDetailsViewModel ToPhotoDetailsViewModel(this BllPhoto photo)
        {
            return new PhotoDetailsViewModel()
            {
                Id = photo.Id,
                Name = photo.Name,
                Description = photo.Description,
                Image = photo.Image,
                NumberOfLikes = photo.NumberOfLikes,
                Tags = photo.Tags,
                UploadDate = photo.UploadDate.ToLocalTime(),
                UserId = photo.UserId,
                UserLikes = photo.UserLikes,
            };
        }

        public static PhotoOwnerViewModel ToPhotoOwnerViewModel(this BllUser user)
        {
            return new PhotoOwnerViewModel()
            {
                NameOwner = user.Login,
                ProfilePhotoOwner = user.ProfilePhoto
            };
        }

        public static PhotoRatingViewModel ToPhotoRatingViewModel(this BllPhoto photo)
        {
            return new PhotoRatingViewModel()
            {
                Id = photo.Id,
                NumberOfLikes = photo.NumberOfLikes
            };
        }

        //public static BllPhoto ToBllPhoto(this PhotoViewModel photo)
        //{
        //    return new BllPhoto()
        //    {
        //        Id = photo.Id,
        //        Name = photo.Name,
        //        Description = photo.Description,
        //        Image = photo.Image,
        //        NumberOfLikes = photo.NumberOfLikes,
        //        Tags = photo.Tags,
        //        UploadDate = photo.UploadDate,
        //        UserId = photo.UserId,
        //        UserLikes = photo.UserLikes
        //    };
        //}

        public static ProfileViewModel ToProfileViewModel(this BllUser user)
        {
            return new ProfileViewModel()
            {
                Email = user.Email,
                Login = user.Login,
                Name = user.Name,
                Phone = user.Phone,
                ProfilePhoto = user.ProfilePhoto
            };
        }

        public static ProfileInfoViewModel ToProfileInfoViewModel(this BllUser user)
        {
            return new ProfileInfoViewModel()
            {
                Name = user.Name,
                ProfilePhoto = user.ProfilePhoto
            };
        }

        public static BllPhoto ToBllPhoto(this UploadPhotoViewModel photo, int userId)
        {

            return new BllPhoto()
            {
                Name = photo.Name,
                Image = ToByteArray(photo.ImageFile),
                Description = photo.Description,
                Tags = ToTags(photo.Tags),
                UploadDate = DateTime.Now,
                UserLikes = new List<int>(0),
                UserId = userId
            };
        }

        public static BllComment ToBllComment(this AddCommentViewModel model, int userId, string userName)
        {
            return new BllComment()
            {
                PhotoId = model.PhotoId,
                Posted = DateTime.Now,
                Text = model.Text,
                Author = new BllAuthor()
                {
                    Id = userId,
                    Name = userName
                }
            };
        }

        public static Author ToAuthor(this BllAuthor author)
        {
            return new Author()
            {
                Id = author.Id,
                Name = author.Name
            };
        }

        public static CommentViewModel ToCommentViewModel(this BllComment comment)
        {
            return new CommentViewModel()
            {
                Id = comment.Id,
                Text = comment.Text,
                Author = comment.Author.ToAuthor()
            };
        }

        public static byte[] ToByteArray(this HttpPostedFileBase photo)
        {
            if (photo == null)
            {
                return new byte[0];
            }
            byte[] thePictureAsBytes = new byte[photo.ContentLength];
            using (BinaryReader theReader = new BinaryReader(photo.InputStream))
            {
                thePictureAsBytes = theReader.ReadBytes(photo.ContentLength);
            }
            return thePictureAsBytes;
        }

        private static IEnumerable<string> ToTags(string tags)
        {
            if (string.IsNullOrEmpty(tags))
                return new string[0];
            string[] splitedTags = tags.Split(' ');
            return splitedTags.Where(s => s.StartsWith("#"));
        }
    }
}