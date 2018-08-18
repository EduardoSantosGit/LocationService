using AutoMapper;
using LocationService.Domain.Common;
using LocationService.Domain.Models;
using LocationService.Domain.Resources;
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
    public class AddressesController : ControllerExtensions
    {
        public SearchAddressService _searchAddressService;

        public AddressesController(SearchAddressService searchAddressService)
        {
            _searchAddressService = searchAddressService;
        }

        [HttpGet("zipCode/{zipcode}")]
        public async Task<IActionResult> GetAdressCode(string zipcode)
        {
            var resultAddress = await _searchAddressService.FindByZipCode(zipcode);

            if(resultAddress.Status == Domain.Common.ResultCode.OK)
            {
                var resource = Mapper.Map<Address, AddressResource>(resultAddress.ValueType);
                return new OkObjectResult(resource);
            }

            return ReturnResult(new Result<string>(resultAddress.Status, resultAddress.Value));
        }

        [HttpGet("term/{term}")]
        public async Task<IActionResult> GetAddressTerm(string term)
        {
            var resultAddresses = await _searchAddressService.FindByTerm(term);

            if (resultAddresses.Status == Domain.Common.ResultCode.OK)
            {
                var lstResour = Mapper.Map<IEnumerable<Address>, IEnumerable<AddressResource>>
                    (
                        resultAddresses.ValueType
                    );

                var resource = new AddressResources { Addresses = lstResour, Count = lstResour.Count() };

                return new OkObjectResult(resource);
            }

            return ReturnResult(new Result<string>(resultAddresses.Status, resultAddresses.Value));
        }
    }
}
