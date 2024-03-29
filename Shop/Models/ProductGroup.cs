﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class ProductGroup
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe a descricao do grupo de produto.")]
        [MaxLength(100)]
        public string Descricao { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();

        public ProductGroup()
        {

        }

        public ProductGroup(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }

        public void AddProducts(Product p)
        {
            Products.Add(p);
        }

        public void RemoveProducts(Product p)
        {
            Products.Remove(p);
        }
    }
}
