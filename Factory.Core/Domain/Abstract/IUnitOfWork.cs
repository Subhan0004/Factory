using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Core.Domain.Abstract
{
    public interface IUnitOfWork
    {
        bool CheckServer();
        ICustomerRepository CustomerRepository { get; }

        ICategoryRepository CategoryRepository { get; }
        
        IUserRepository UserRepository { get; }
        
        IProductRepository ProductRepository { get; }

    }
}
