using LocationService.Domain.Interfaces;
using LocationService.Domain.Models;
using LocationService.Infrastructure.Common;
using LocationService.Infrastructure.Services.Provider;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

            return null;
        }

        public async Task<List<Adress>> GetAdressesTerm(string term)
        {

            

            return null;
        }

    }
}
