using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IDiscountRepository
    {

        Task<int> CountDiscountsByUserAndDateAsync(int userId, DateTime date);
        Task<Discount> CreateAsync(Discount discountModel);
        Task<Discount?> GetByIdAsync(int id);

        Task SaveChangesAsync();
        Task UpdateAsync(Discount discount);
        Task<Discount?> GetValidDiscountForCustomer(int customerId, DateTime purchaseDate);
    }
}