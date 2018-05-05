using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.DTO
{
    public class DalUser : IEntity
    {
        public int Id { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public DalProfilePhoto DalProfilePhoto { get; set; }
    }
}
