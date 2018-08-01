using LocationService.Domain.Common;
using LocationService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LocationService.Domain.Interfaces
{
    public interface IAddressesServices
    {
        Task<Result<Address>> GetAddressesZipCode(string zipCode);
        Task<Result<IEnumerable<Address>>> GetAddressesTerm(string term);
    }
}
