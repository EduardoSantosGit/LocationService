using LocationService.Domain.Services.Addresses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationService.Api.Controllers
{
    [Route("api/v1/address/")]
    [Produces("application/json")]
    public class AddressesController : Controller
    {
        public SearchAddressService _searchAddressService;

        public AddressesController(SearchAddressService searchAddressService)
        {
            _searchAddressService = searchAddressService;
        }

        [HttpGet("zipCode/{zipcode}")]
        public async Task<IActionResult> GetAdressCode(string zipcode)
        {
            var values = await _searchAddressService.FindByZipCode(zipcode);
            return new OkObjectResult(values);
        }

        [HttpGet("term/{term}")]
        public async Task<IActionResult> GetAddressTerm(string term)
        {
            var values = await _searchAddressService.FindByTerm(term);
            return new OkObjectResult(values);
        }
    }
}
