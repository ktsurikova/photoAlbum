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

        private static byte[] ToByteArray(HttpPostedFileBase photo)
        {
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