using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMugTest.Models
{
    public class ProductItem
    {
        public long Id { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Name is required")]
        public string Name  { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }
    }
}
