using Factory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factory.ViewModels
{
    public class ProductViewModel
    {
        public List<ProductModel> Products { get; set; } = new List<ProductModel>();

        public int DeletedId { get; set; }
    }
}
