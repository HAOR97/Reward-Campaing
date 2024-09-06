using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Dtos.Discount;
using api.Models;

namespace api.Mappers
{
    public static class DiscountMappers
    {
        public static DiscountDto toDiscountDto(this Discount discountModel)
        {
            return new DiscountDto
            {
                Id = discountModel.Id,
                UserId = discountModel.UserId,
                CustomerId = discountModel.CustomerId,
                Created = discountModel.Created,
                DiscountAmount = discountModel.DiscountAmount,
                IsUsed = discountModel.IsUsed
            };

        }
        public static Discount ToDiscountFromCreateDTO(this CreateDiscountRequestDto requestDto)
        {
            return new Discount
            {
                
                UserId = requestDto.UserId,
                CustomerId = requestDto.CustomerId,
                Created = DateTime.UtcNow,
                DiscountAmount = requestDto.DiscountAmount,
                IsUsed = false
            };
        }



    }
}