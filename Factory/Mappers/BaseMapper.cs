using Factory.Core.Domain.Entities;
using Factory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factory.Mappers
{
    public abstract class BaseMapper<T1, T2> where T1 : BaseEntity where T2 : BaseModel
    {
        public abstract T1 Map(T2 t);

        public abstract T2 Map(T1 t);

    }
}
