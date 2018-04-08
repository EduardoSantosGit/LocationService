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
        private readonly ICacheManager<Adress> _cache;
        private readonly ICacheManager<List<Adress>> _cacheLst;

        public AdressesService(IEnumerable<IAddressProvider> addressProvider, ICacheManager<Adress> cache, ICacheManager<List<Adress>> cacheLst)
        {
            _addressProvider = addressProvider;
            _cache = cache;
            _cacheLst = cacheLst;
        }

        public async Task<Adress> GetAdressesZipCode(string zipCode)
        {
            var serAvailable = _addressProvider.Count();
            var result = default(Adress);

            if (_cache.Get(zipCode) != null)
                return result;

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
            _cache.Add(zipCode, result);
            return result;
        }

        public async Task<List<Adress>> GetAdressesTerm(string term)
        {
            var serAvailable = _addressProvider.Count();
            var result = new List<Adress>();

            if (_cacheLst.Get(term) != null)
                return result;

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
            _cacheLst.Add(term, result);
            return result;
        }

    }
}
