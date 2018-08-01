using LocationService.Domain.Interfaces;
using LocationService.Domain.Models;
using LocationService.Infrastructure.Common;
using LocationService.Infrastructure.Services.Provider;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using CacheManager.Core;
using LocationService.Domain.Common;

namespace LocationService.Infrastructure.Services.Addresses
{
    public class AddressesService : IAddressesServices
    {
        private readonly IEnumerable<IAddressProvider> _addressProvider;

        public AddressesService(IEnumerable<IAddressProvider> addressProvider)
        {
            _addressProvider = addressProvider;
        }

        public async Task<Result<Address>> GetAddressesZipCode(string zipCode)
        {
            var serAvailable = _addressProvider.Count();
            var serviceUsed = _addressProvider.First();

            var result = await serviceUsed.GetAddressesZipCode(zipCode);

            if(result.Status != ResultCode.OK && serAvailable > 1)
            {
                foreach (var item in _addressProvider.Skip(1))
                {
                    result = await item.GetAddressesZipCode(zipCode);

                    if (result.Status == ResultCode.OK)
                        break;
                }
            }

            if (result.Status == ResultCode.OK)
                return new Result<Address>(ResultCode.OK, result.ValueType);

            return new Result<Address>(result.Status, result.Value);
        }

        public async Task<Result<IEnumerable<Address>>> GetAddressesTerm(string term)
        {
            var serAvailable = _addressProvider.Count();
            var serviceUsed = _addressProvider.First();

            var result = await serviceUsed.GetAddressesTerm(term);

            if (result.Status != ResultCode.OK && serAvailable > 1)
            {
                foreach (var item in _addressProvider.Skip(1))
                {
                    result = await item.GetAddressesTerm(term);

                    if (result.Status == ResultCode.OK)
                        break;
                }
            }

            if (result.Status == ResultCode.OK)
                return new Result<IEnumerable<Address>>(ResultCode.OK, result.ValueType);

            return new Result<IEnumerable<Address>>(result.Status, result.Value);
        }

    }
}
