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
        public readonly string _baseUrl;
        public readonly string _apiUrl;

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
            var result = await this.GetSendAsync(zipCode);

            if(result.Status == ResultCode.OK)
            {
                var routePostal = JsonConvert.DeserializeObject<AddressRoutePostal>(result.ValueType);
                return new Result<Address>(ResultCode.OK, Map.ConvertRouteAsAdress(routePostal));
            }

            return new Result<Address>(result.Status, result.Value);
        }
    }
}
