using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class UserGroup
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe a descricao do grupo de usuários.")]
        [MaxLength(100)]
        public string Descricao { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();

    }
}
