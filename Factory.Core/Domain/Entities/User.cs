using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Core.Domain.Entities
{
   public class User :BaseEntity
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public bool CanOperatorCrm { get; set; }

        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
