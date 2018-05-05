using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class PhotoOwnerViewModel
    {
        public string NameOwner { get; set; }
        public string Url { get; set; }
        public string ImageType { get; set; }

        public string GetUrlWithSize(int size)
        {
            if (ImageType == "google")
                return Url += $"?sz={size}";
            return Url;
        }
    }
}