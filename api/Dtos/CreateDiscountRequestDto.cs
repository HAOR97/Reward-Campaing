using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Discount
{
    public class CreateDiscountRequestDto
    {   
        [Required]
        public int UserId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public decimal DiscountAmount { get; set; }

    }
}