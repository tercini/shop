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

        public UserGroup()
        {

        }

        public UserGroup(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }

        public void AddUsers(User u)
        {
            Users.Add(u);
        }

        public void RemoveUsers(User u)
        {
            Users.Remove(u);
        }

    }
}
