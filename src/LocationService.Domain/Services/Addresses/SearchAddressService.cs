using LocationService.Domain.Interfaces;
using LocationService.Domain.Models;
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

        public async Task<Address> FindByZipCode(string zipCode)
        {
            var result = await _addressesServices.GetAddressesZipCode(zipCode);

            if(result.Status == Common.ResultCode.OK)
            {
                
            }

            return result.ValueType;
        }
        

        public async Task<IEnumerable<Address>> FindByTerm(string term)
        {
            var result = await _addressesServices.GetAddressesTerm(term);
            return result.ValueType;
        }
        
    }
}
