using Factory.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Core.Domain.Abstract
{
    public interface IProductRepository
    {
        int Add(Product product);
        bool Update(Product product);
        List<Product> Get();
        Product FindById(int id);
    }
}
