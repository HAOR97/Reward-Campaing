using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Purchase
    {
        public int CustomerId { get; set; }

        public DateTime PurchaseDate { get; set; }
        public string Product { get; set; }  = string.Empty;
        public decimal PurchaseAmount { get; set; }
    }
}