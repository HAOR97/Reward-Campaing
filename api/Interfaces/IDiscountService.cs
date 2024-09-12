using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Dtos.Discount;

namespace api.Interfaces
{
    public interface IDiscountService
    {
        Task<DiscountDto> GetByIdAsync(int id);
        Task<bool> CanAssignMoreDiscountsAsync(int userId);
        Task<DiscountDto> CreateDiscountAsync(CreateDiscountRequestDto discountDto);
    }
}