using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class PaginationViewModel<T> where T : class
    {
        public IEnumerable<T> Items { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}