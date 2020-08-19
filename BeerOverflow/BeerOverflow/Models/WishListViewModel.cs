using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflow.Models
{
    public class WishListViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Country { get; set; }

        public ICollection<BeerViewModel> WishLists { get; set; }
        public ICollection<BeerViewModel> DrankLists { get; set; }
    }
}
