using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.Services;  // Pretpostavimo da je SoapClientService u ovom namespace-u
using api.Interfaces;

namespace api.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ISoapClientService _soapClientService;

        public CustomerController(ISoapClientService soapClientService)
        {
            _soapClientService = soapClientService;
        }

        // API metoda koja koristi SoapClientService da preuzme podatke o korisniku po ID-u
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(id);

            var result = await _soapClientService.FindPersonAsync(id);

            if(string.IsNullOrEmpty(result))
            {
                return NotFound("Person not found.");
            }

            return Ok(result);
            
        }
    }
}
