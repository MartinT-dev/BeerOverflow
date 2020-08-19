using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflow.Areas.Admin.Models
{
    public class ReviewAdminViewModel
    {
        public int Id { get; set; }
        [DisplayName("Title")]
        public string Title { get; set; }
        [DisplayName("Text")]
        public string Text { get; set; }

        [DisplayName("User")]
        public string User { get; set; }
        [DisplayName("Beer")]
        public string Beer { get; set; }

        public bool isDeleted { get; set; }
        public bool isFlagged { get; set; }
    }
}
