using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflow.Services.DTOs
{
    public class RatingDTO
    {
        public int UserId { get; set; }
        public string User { get; set; }
        public int BeerId { get; set; }
        public string Beer { get; set; }

        public double RatingValue { get; set; }
    }
}
