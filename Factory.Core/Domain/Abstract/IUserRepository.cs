using Factory.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Core.Domain.Abstract
{
    public interface IUserRepository
    {
        User Get(string username);
        User Get(int id);
    }
}
