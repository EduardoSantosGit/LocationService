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
    public class AdressesMailService : IAdressesServices
    {
        public readonly ClientMailApi _adressesClientApi;

        public AdressesMailService()
        {
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
