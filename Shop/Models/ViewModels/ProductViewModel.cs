using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public ICollection<ProductGroup> ProductGroups { get; set; }
        [NotMapped]
        public IFormFile ImgDoc { get; set; }

    }
}
