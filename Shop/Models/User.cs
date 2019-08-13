using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe a descricao do grupo de usuários.")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Informe a descricao do produto.")]
        [MaxLength(100)]
        public string Email { get; set; }

        public string Senha { get; set; }

        public UserGroup UserGroup { get; set; }

        public ICollection<Address> Adresses { get; set; } = new List<Address>();

        public ICollection<Sale> Sales { get; set; } = new List<Sale>();

    }
}
