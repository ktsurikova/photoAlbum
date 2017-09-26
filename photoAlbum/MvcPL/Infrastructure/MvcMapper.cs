using System;
using System.Collections.Generic;
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
    }
}