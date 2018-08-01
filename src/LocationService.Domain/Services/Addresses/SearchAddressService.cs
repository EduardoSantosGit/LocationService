using LocationService.Domain.Common;
using LocationService.Domain.Interfaces;
using LocationService.Domain.Models;
using LocationService.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LocationService.Domain.Services.Addresses
{
    public class SearchAddressService
    {
        public IAddressesServices _addressesServices;

        public SearchAddressService(IAddressesServices addressesServices)
        {
            _addressesServices = addressesServices;
        }

        public async Task<Result<Address>> FindByZipCode(string zipCode)
        {
            var result = await _addressesServices.GetAddressesZipCode(zipCode);

            if(result.Status == ResultCode.OK)
                return new Result<Address>(ResultCode.OK, result.ValueType);

            return new Result<Address>(result.Status, result.Value);
        }
        

        public async Task<Result<IEnumerable<Address>>> FindByTerm(string term)
        {
            var result = await _addressesServices.GetAddressesTerm(term);

            if (result.Status == ResultCode.OK)
                return new Result<IEnumerable<Address>>(ResultCode.OK, result.ValueType);

            return new Result<IEnumerable<Address>>(result.Status, result.Value);
        }
        
    }
}
