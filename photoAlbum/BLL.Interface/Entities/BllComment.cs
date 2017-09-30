using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    public class BllComment
    {
        public int Id { get; set; }
        public int PhotoId { get; set; }
        public DateTime Posted { get; set; }
        public string Text { get; set; }
        public BllAuthor Author { get; set; }
    }
}
