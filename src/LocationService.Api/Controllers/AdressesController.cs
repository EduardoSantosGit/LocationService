using LocationService.Domain.Services.Adresses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationService.Api.Controllers
{
    [Route("api/v1/adress/")]
    [Produces("application/json")]
    public class AdressesController : Controller
    {
        public SearchAdressService _searchAdressService;

        public AdressesController(SearchAdressService searchAdressService)
        {
            _searchAdressService = searchAdressService;
        }

        [HttpGet("zipCode/{zipcode}")]
        public async Task<IActionResult> GetAdressCode(string zipcode)
        {
            var values = await _searchAdressService.FindByZipCode(zipcode);
            return new OkObjectResult(values);
        }

        [HttpGet("term/{term}")]
        public async Task<IActionResult> GetAdressTerm(string term)
        {
            var values = await _searchAdressService.FindByTerm(term);
            return new OkObjectResult(values);
        }
    }
}
