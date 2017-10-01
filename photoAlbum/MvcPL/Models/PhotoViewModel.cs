using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class PhotoViewModel
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
    }
}