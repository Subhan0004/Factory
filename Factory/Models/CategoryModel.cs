using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Factory.Models
{
    public class CategoryModel : BaseModel
    {
        [Required(ErrorMessage = "The name must be entered!")]
        [StringLength(50, ErrorMessage = "The name can't be longer than 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The note must be entered!")]
        public string Note { get; set; }
    }
}
