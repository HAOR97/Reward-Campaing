using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly ApplicationDBContext _context;
        public DiscountRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<int> CountDiscountsByUserAndDateAsync(int userId, DateTime date)
        {
            return await _context.Discount
                                 .Where(d => d.UserId == userId && d.Created.Date == date.Date)
                                 .CountAsync();
        }

        public async Task<Discount> CreateAsync(Discount discountModel)
        {
            await _context.Discount.AddAsync(discountModel);
            await _context.SaveChangesAsync();

            return discountModel;
        }
        public async Task<Discount?> GetByIdAsync(int id)
        {
            return await _context.Discount.FindAsync(id);
        }
        public async Task<Discount?> GetValidDiscountForCustomer(int customerId, DateTime purchaseDate)
        {

            return await _context.Discount
                .Where(d => d.CustomerId == customerId 
                && !d.IsUsed 
                && d.Created <= purchaseDate
                )
                .OrderByDescending(d => d.Created)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Discount discount)
        {
            _context.Discount.Update(discount);
            await _context.SaveChangesAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}