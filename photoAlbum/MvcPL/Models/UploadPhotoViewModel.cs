using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class UploadPhotoViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        //public byte[] Image { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}