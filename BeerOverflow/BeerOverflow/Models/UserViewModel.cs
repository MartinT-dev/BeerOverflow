using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflow.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Country { get; set; }
        public ICollection<string> WishLists { get; set; }
        public ICollection<string> DrankLists { get; set; }
    }
}
