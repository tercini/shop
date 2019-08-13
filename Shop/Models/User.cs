using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;

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

        public int UserGroupId { get; set; }

        public UserGroup UserGroup { get; set; }

        public ICollection<Address> Addresses { get; set; } = new List<Address>();

        public ICollection<Sale> Sales { get; set; } = new List<Sale>();

        public User()
        {

        }

        public User(int id, string name, string email, string senha, UserGroup userGroup)
        {
            Id = id;
            Name = name;
            Email = email;
            Senha = senha;
            UserGroup = userGroup;
        }

        public void AddSales(Sale s)
        {
            Sales.Add(s);
        }
        
        public void RemoveSales(Sale s)
        {
            Sales.Remove(s);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(s => s.Date >= initial && s.Date <= final).Sum(s => s.Total);
        }

        public void AddAdresses(Address a)
        {
            Addresses.Add(a);
        }

        public void RemoveAdresses(Address a)
        {
            Addresses.Remove(a);
        }

    }
}
