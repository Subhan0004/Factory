using Factory.Core.Domain.Entities;
using Factory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factory.Mappers
{
    public class CustomerMapper : BaseMapper<Customer, CustomerModel>
    {
        public override Customer Map(CustomerModel customerModel)
        {
            if (customerModel == null)
                return null;
            Customer customer = new Customer();

            customer.Name = customerModel.Name;
            customer.Surname = customerModel.Surname;
            customer.Phone = customerModel.Phone;
            customer.Address = customerModel.Address;
            customer.Note = customerModel.Note;
            customer.Id = customerModel.Id;

            return customer;
        }

        public override CustomerModel Map(Customer customer)
        {
            if (customer == null)
                return null;
            CustomerModel customerModel = new CustomerModel();

            customerModel.Id = customer.Id;
            customerModel.Name = customer.Name;
            customerModel.Surname = customer.Surname;
            customerModel.Phone = customer.Phone;
            customerModel.Address = customer.Address;
            customerModel.Note = customer.Note;

            return customerModel;

        }
    }
}
