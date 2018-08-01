using LocationService.Domain.Common;
using LocationService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LocationService.Domain.Interfaces
{
    public interface IAddressProvider
    {
        Task<Result<Address>> GetAddressesZipCode(string zipCode);
        Task<Result<List<Address>>> GetAddressesTerm(string term);
    }
}
