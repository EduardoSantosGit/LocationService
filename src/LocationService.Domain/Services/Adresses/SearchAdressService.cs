using LocationService.Domain.Interfaces;
using LocationService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LocationService.Domain.Services.Adresses
{
    public class SearchAdressService
    {
        public IAdressesServices _adressesServices;

        public SearchAdressService(IAdressesServices adressesServices)
        {
            _adressesServices = adressesServices;
        }

        public async Task<Adress> FindByZipCode(string zipCode)
        {
            return await _adressesServices.GetAdressesZipCode(zipCode);
        }

        public async Task<List<Adress>> FindByTerm(string term)
        {
            return await _adressesServices.GetAdressesTerm(term);
        }
    }
}
