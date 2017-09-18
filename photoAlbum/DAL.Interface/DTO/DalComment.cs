using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.DTO
{
    public class DalComment : IEntity
    {
        public int Id { get; set; }
        public int PhotoId { get; set; }
        public DateTime Posted { get; set; }
        public string Text { get; set; }
        public DalAuthor Author { get; set; }
    }
}
