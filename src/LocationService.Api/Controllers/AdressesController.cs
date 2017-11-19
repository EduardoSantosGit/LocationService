using LocationService.Domain.Services.Adresses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationService.Api.Controllers
{
    [Route("api/v1/adress/")]
    public class AdressesController : Controller
    {

        public SearchAdressService _searchAdressService;

        public AdressesController(SearchAdressService searchAdressService)
        {
            _searchAdressService = searchAdressService;
        }

        [HttpGet("zipCode/{zipcode}")]
        public async Task<JsonResult> GetWorldsMarkets(string zipcode)
        {
            var values = await _searchAdressService.FindByZipCode(zipcode);
            return Json(values);
        }
    }
}
