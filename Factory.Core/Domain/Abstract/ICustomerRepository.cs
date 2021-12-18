using Factory.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Core.Domain.Abstract
{
    public interface ICustomerRepository
    {
        int Add(Customer customer);
        bool Update(Customer customer);
        List<Customer> Get();
        Customer Get(int id);
    }
}
