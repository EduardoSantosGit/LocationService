using LocationService.Domain.Interfaces;
using LocationService.Domain.Models;
using LocationService.Infrastructure.Common;
using LocationService.Infrastructure.Services.Provider;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

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
            var ieAdress = default(IEnumerable<Adress>);
            var result = default(Adress);

            if (serAvailable > 0)
            {
                var providerOne = _addressProvider.First();
                result = await providerOne.GetAdressesZipCode(zipCode);
                
                if (result == null)
                {
                    ieAdress = _addressProvider.Skip(1).Select(x => x.GetAdressesZipCode(zipCode).Result)
                                    .Where(y => y != null);

                    if (ieAdress.Count() > 0)
                        return ieAdress.ElementAt(0);
                }

            }
              
            return result;
        }

        public async Task<List<Adress>> GetAdressesTerm(string term)
        {
            var serAvailable = _addressProvider.Count();

            if (serAvailable > 0)
            {
                var providerOne = _addressProvider.First();
                var result = await providerOne.GetAdressesZipCode(term);
                var ieAdress = default(IEnumerable<List<Adress>>);

                if (result == null)
                {
                   
                }

            }

            return null;
        }

    }
}
