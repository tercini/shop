using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }        

        public double Amount { get; set; }

        public double Total { get; set; }        

        public Product Product { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }
    }
}
