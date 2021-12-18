using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Core.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Note { get; set; }
    }
}
