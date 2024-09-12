using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Discount;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{

    [Route("api/discount")]
    [ApiController]
    public class DiscountController : ControllerBase
    {

        private readonly IDiscountService _discountService;
        private readonly ICsvService _csvService;

        public DiscountController(IDiscountService discountService, ICsvService csvService)
        {
            _discountService = discountService;
            _csvService = csvService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(id);

            var discount = await _discountService.GetByIdAsync(id);
            if (discount == null)
                return NotFound(new { message = "Popust nije pronađen." });

            return Ok(discount);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDiscountRequestDto discountDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(discountDto);

            var canAssign = await _discountService.CanAssignMoreDiscountsAsync(discountDto.UserId);
            if (!canAssign)
            {
                return BadRequest(new { message = "Ne možete dodeliti više od 5 popusta u jednom danu." });
            }

            var createdDiscount = await _discountService.CreateDiscountAsync(discountDto);
            return CreatedAtAction(nameof(GetById), new { id = createdDiscount.Id }, createdDiscount);
        }

        [HttpPost("upload-csv")]
        public async Task<IActionResult> UploadCsv(IFormFile file)
        {
            var allowedExtensions = new[] { ".csv" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
                return BadRequest("Invalid file type. Only .csv files are allowed.");

            if (file == null || file.Length == 0)
                return BadRequest("File is empty or not provided.");

            var result = await _csvService.ProcessCsvFile(file);
            return Ok(result);
        }


    }
}