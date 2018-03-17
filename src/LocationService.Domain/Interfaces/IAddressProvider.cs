﻿using LocationService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LocationService.Domain.Interfaces
{
    public interface IAddressProvider
    {

        Task<Adress> GetAdressesZipCode(string zipCode);
        Task<List<Adress>> GetAdressesTerm(string term);
        
    }
}