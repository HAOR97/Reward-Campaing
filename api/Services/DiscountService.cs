using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using api.Dtos;
using api.Dtos.Discount;
using api.Interfaces;
using api.Mappers;

namespace api.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepo;

        public DiscountService(IDiscountRepository discountRepo)
        {
            _discountRepo = discountRepo;
        }

        public async Task<DiscountDto> GetByIdAsync(int id)
        {
            var discount = await _discountRepo.GetByIdAsync(id);
            if (discount == null) return null;

            return discount.toDiscountDto();
        }

        public async Task<bool> CanAssignMoreDiscountsAsync(int userId)
        {
            var today = DateTime.UtcNow;
            var count = await _discountRepo.CountDiscountsByUserAndDateAsync(userId, today);
            return count < 5;
        }

        public async Task<DiscountDto> CreateDiscountAsync(CreateDiscountRequestDto discountDto)
        {
            var discountModel = discountDto.ToDiscountFromCreateDTO();
            await _discountRepo.CreateAsync(discountModel);
            return discountModel.toDiscountDto();
        }
    }
}