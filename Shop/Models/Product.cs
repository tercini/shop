using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe a descricao do produto.")]
        [MaxLength(100)]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informe o valor do produto.")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:#,##0.000#}", ApplyFormatInEditMode = true)]
        //[DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public double Valor { get; set; }

        [MaxLength(160)]
        public string Observacao { get; set; }

        public UserGroup UserGroup { get; set; }

        public ICollection<ItemSale> ItemsSales {get; set;} 

        public Product()
        {

        }

        public Product(int id, string descricao, double valor, string observacao, UserGroup userGroup)
        {
            Id = id;
            Descricao = descricao;
            Valor = valor;
            Observacao = observacao;
            UserGroup = userGroup;
        }
    }
}
