using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Author Author { get; set; }
    }
}