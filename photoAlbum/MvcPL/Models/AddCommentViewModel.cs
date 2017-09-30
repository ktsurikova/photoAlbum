using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class AddCommentViewModel
    {
        public int PhotoId { get; set; }
        public string Text { get; set; }
    }
}