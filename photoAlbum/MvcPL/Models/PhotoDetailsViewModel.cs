using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class PhotoDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfLikes { get; set; }
        public DateTime UploadDate { get; set; }
        public int UserId { get; set; }
        public PhotoOwnerViewModel Owner { get; set; }
        public int CurrentUserId { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public IEnumerable<int> UserLikes { get; set; }
        public byte[] Image { get; set; }
    }
}