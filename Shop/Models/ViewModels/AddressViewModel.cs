using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models.ViewModels
{
    public class AddressViewModel
    {
        public Address Address { get; set; }
        public ICollection<User> User { get; set; }
    }
}
