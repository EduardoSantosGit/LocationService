using LocationService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LocationService.Domain.Interfaces
{
    public interface IAdressesServices
    {
        Task<Adress> GetAdressesScrap(string zipCode);
    }
}
