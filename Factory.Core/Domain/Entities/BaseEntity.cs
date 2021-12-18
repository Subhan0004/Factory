using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Core.Domain.Entities
{
    public class BaseEntity
    {
         public int Id { get; set; }

        public User Creator { get; set; }

        public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; }
    }
}
