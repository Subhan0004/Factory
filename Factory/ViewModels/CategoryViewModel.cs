using Factory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factory.ViewModels
{
    public class CategoryViewModel
    {
        public List<CategoryModel> Categories { get; set; } = new List<CategoryModel>();

        public int DeletedId { get; set; }

    }
}
