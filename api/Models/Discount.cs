using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Discount
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CustomerId { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public decimal DiscountAmount { get; set; }
        public bool IsUsed { get; set; } = false; 
    }
}