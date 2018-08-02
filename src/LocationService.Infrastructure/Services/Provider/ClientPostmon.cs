using LocationService.Domain.Common;
using LocationService.Domain.Interfaces;
using LocationService.Domain.Models;
using LocationService.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LocationService.Infrastructure.Services.Provider
{
    public class ClientPostmon : ProviderHttp, IAddressProvider
    {

        public ClientPostmon(string baseUrl, TimeSpan timeout) 
            : base(baseUrl, timeout)
        {
            //http://api.postmon.com.br/v1/
        }

        public Task<Result<List<Address>>> GetAddressesTerm(string term)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Address>> GetAddressesZipCode(string zipCode)
        {
            throw new NotImplementedException();
        }
    }
}
