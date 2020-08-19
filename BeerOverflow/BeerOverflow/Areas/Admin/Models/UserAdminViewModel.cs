using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflow.Areas.Admin.Models
{
    public class UserAdminViewModel
    {
        public int Id { get; set; }
        [DisplayName("UserName")]
        public string Username { get; set; }
        [DisplayName("Country")]
        public string Country { get; set; }
        [DisplayName("Banned")]
        public bool isBanned { get; set; }
    }
}
