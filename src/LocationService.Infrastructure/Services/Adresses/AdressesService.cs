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

namespace LocationService.Infrastructure.Services.Adresses
{
    public class AdressesService : IAdressesServices
    {
        private readonly IEnumerable<IAddressProvider> _addressProvider;

        public AdressesService(IEnumerable<IAddressProvider> addressProvider)
        {
            _addressProvider = addressProvider;
        }

        public async Task<Adress> GetAdressesZipCode(string zipCode)
        {
            var serAvailable = _addressProvider.Count();
            var result = default(Adress);

            if (serAvailable > 0)
            {
                var providerOne = _addressProvider.First();
                result = await providerOne.GetAdressesZipCode(zipCode);

                if (result == null)
                {
                    var otherProvider = _addressProvider.Skip(1);

                    foreach (var item in otherProvider)
                    {
                        result = await item.GetAdressesZipCode(zipCode);

                        if (result != null)
                        {
                            break;
                        }
                    }
                }
            }
            return result;
        }

        public async Task<IEnumerable<Adress>> GetAdressesTerm(string term)
        {
            var serAvailable = _addressProvider.Count();
            var result = new List<Adress>();

            if (serAvailable > 0)
            {
                var providerOne = _addressProvider.First();
                result = await providerOne.GetAdressesTerm(term);

                if (result.Count == 0)
                {
                    var otherProvider = _addressProvider.Skip(1);

                    foreach (var item in otherProvider)
                    {
                        result = await item.GetAdressesTerm(term);

                        if (result.Count > 0)
                        {
                            break;
                        }
                    }
                }
            }
            return result;
        }

    }
}
