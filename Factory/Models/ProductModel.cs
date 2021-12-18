using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Factory.Models
{
    public class ProductModel : BaseModel
    {
        [Required(ErrorMessage = "The name must be entered!")]
        [StringLength(200, ErrorMessage = "The name can't be longer than 10 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The category must be entered!")]
        public CategoryModel Category { get; set; }

        [Required(ErrorMessage = "The price must be entered!")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        [Required(ErrorMessage = "The quantity must be entered!")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "The note must be entered!")]
        public string Note { get; set; }

        public List<SelectListItem> Categories;
    }
}
