using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class EditProfileViewModel
    {
        public string Name { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}