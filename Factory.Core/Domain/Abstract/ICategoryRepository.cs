using Factory.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Core.Domain.Abstract
{
    public interface ICategoryRepository
    {
        int Add(Category category);
        bool Update(Category category);
        List<Category> Get();
        Category Get(int id);
    }
}
