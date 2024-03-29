﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public User User { get; set; }
        
        public decimal? Total { get; set; }

        public ICollection<ItemSale> SalesItens { get; set; } = new List<ItemSale>();

        public Sale()
        {

        }

        public Sale(int id, DateTime date, decimal total, User user)
        {
            Id = id;
            Date = date;
            Total = total;
            User = user;
        }
    }
}
