using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Models.ViewModels
{
    public class UserViewModel
    {
        public User User { get; set; }
        public ICollection<UserGroup> UserGroups { get; set; }
    }
}
