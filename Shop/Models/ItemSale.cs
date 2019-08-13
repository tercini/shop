using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class ItemSale
    {
        [Key]
        public int Id { get; set; }

        public double Amount { get; set; }

        public double Total { get; set; }

        public Sale Sale { get; set; }

        public Product Product { get; set; }

        public ItemSale()
        {

        }

        public ItemSale(int id, double amount, double total, Sale sale, Product product)
        {
            Id = id;
            Amount = amount;
            Total = total;
            Sale = sale;
            Product = product;
        }
    }
}
