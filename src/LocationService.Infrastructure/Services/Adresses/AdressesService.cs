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

namespace LocationService.Infrastructure.Services.Adresses
{
    public class AdressesService : IAdressesServices
    {
        private readonly IEnumerable<IAddressProvider> _addressProvider;

        public AdressesService(IEnumerable<IAddressProvider> addressProvider)
        {
            _addressProvider = addressProvider;
        }

        public async Task<Result<Adress>> GetAdressesZipCode(string zipCode)
        {
            var serAvailable = _addressProvider.Count();
            var serviceUsed = _addressProvider.First();

            var result = await serviceUsed.GetAdressesZipCode(zipCode);

            if(result.Status != ResultCode.OK && serAvailable > 1)
            {
                foreach (var item in _addressProvider.Skip(1))
                {
                    result = await item.GetAdressesZipCode(zipCode);

                    if (result.Status == ResultCode.OK)
                        break;
                }
            }

            if (result.Status == ResultCode.OK)
                return new Result<Adress>(ResultCode.OK, result.ValueType);

            return new Result<Adress>(result.Status, result.Value);
        }

        public async Task<Result<IEnumerable<Adress>>> GetAdressesTerm(string term)
        {
            var serAvailable = _addressProvider.Count();
            var serviceUsed = _addressProvider.First();

            var result = await serviceUsed.GetAdressesTerm(term);

            if (result.Status != ResultCode.OK && serAvailable > 1)
            {
                foreach (var item in _addressProvider.Skip(1))
                {
                    result = await item.GetAdressesTerm(term);

                    if (result.Status == ResultCode.OK)
                        break;
                }
            }

            if (result.Status == ResultCode.OK)
                return new Result<IEnumerable<Adress>>(ResultCode.OK, result.ValueType);

            return new Result<IEnumerable<Adress>>(result.Status, result.Value);
        }

    }
}
