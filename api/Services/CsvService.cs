using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Interfaces;
using api.Mappers;
using api.Models;
using CsvHelper;
using CsvHelper.TypeConversion;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace api.Services
{
    public class CsvService : ICsvService
    {
        private readonly IDiscountRepository _discountRepository;

        public CsvService(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        public async Task<List<PurchaseDto>> ProcessCsvFile(IFormFile file)
        {
            var purchases = new List<PurchaseDto>();
            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var dateOptions = new TypeConverterOptions { Formats = new[] { "dd/MM/yyyy" } };
                csv.Context.TypeConverterOptionsCache.AddOptions<DateTime>(dateOptions);

                var records = csv.GetRecords<Purchase>().ToList();
                foreach (var record in records)
                {


                    var discount = await _discountRepository.GetValidDiscountForCustomer(record.CustomerId, record.PurchaseDate);
                    bool hasDiscount = discount != null && discount.Created.Date <= record.PurchaseDate.Date && !discount.IsUsed;


                    if (hasDiscount && discount != null)
                    {
                        var purchase = new PurchaseDto
                        {
                            CustomerId = record.CustomerId,
                            PurchaseDate = record.PurchaseDate,
                            Product = record.Product,
                            DiscountId = discount.Id,
                            DiscountAmount = discount.DiscountAmount
                        };

                        discount.IsUsed = true;
                        await _discountRepository.UpdateAsync(discount);
                        purchases.Add(purchase);
                    }

                }
            }
            return purchases;
        }
    }
}

//primer csv fajla
// CustomerID,PurchaseDate,Product,PurchaseAmount,DiscountUsed
// 12345,04/09/2024(date),Phone,500,Yes
// 67890,04/09/2024,Tablet,300,No
// 54321,04/09/2024,Headset,100,Yes
// 98765,04/09/2024,Laptop,1200,Yes
// 13579,04/09/2024,Smartwatch,250,No
// 24680,04/09/2024,Phone,550,Yes