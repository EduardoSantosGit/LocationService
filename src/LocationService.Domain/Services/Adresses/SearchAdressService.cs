﻿using LocationService.Domain.Interfaces;
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
            => await _adressesServices.GetAdressesZipCode(zipCode);
        

        public async Task<IEnumerable<Adress>> FindByTerm(string term)
            => await _adressesServices.GetAdressesTerm(term);
        
    }
}
