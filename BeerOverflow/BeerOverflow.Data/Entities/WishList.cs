using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflow.Data.Entities
{
    public class WishList
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int BeerId { get; set; }
        public Beer Beer { get; set; }
    }
}
