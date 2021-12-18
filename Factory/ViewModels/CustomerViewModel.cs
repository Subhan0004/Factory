using Factory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factory.ViewModels
{
    public class CustomerViewModel
    {
        public List<CustomerModel> Customers { get; set; } = new List<CustomerModel>();

        public int DeletedId { get; set; }

    }
}
