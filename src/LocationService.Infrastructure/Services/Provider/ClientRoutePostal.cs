using LocationService.Domain.Common;
using LocationService.Domain.Interfaces;
using LocationService.Domain.Models;
using LocationService.Infrastructure.Common;
using LocationService.Infrastructure.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LocationService.Infrastructure.Services.Provider
{
    public class ClientRoutePostal : ProviderHttp, IAddressProvider
    {
        public readonly string _baseUrl;
        public readonly string _apiUrl;

        public ClientRoutePostal(string baseUrl, TimeSpan timeout) : base(baseUrl, timeout)
        {
            _baseUrl = "https://viacep.com.br/";
            _apiUrl = "ws/";
        }

        public async Task<Result<List<Adress>>> GetAdressesTerm(string term)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<string>> GetSendAsync(string zipCode)
        {
            var result = await this.GetAsync($@"{_baseUrl}{_apiUrl}{zipCode}/json");
            return await ResultOperations.ReadHttpResult(result);
        }

        public async Task<Result<Adress>> GetAdressesZipCode(string zipCode)
        {
            var result = await this.GetSendAsync(zipCode);

            if(result.Status == ResultCode.OK)
            {
                var routePostal = JsonConvert.DeserializeObject<AdressRoutePostal>(result.ValueType);
                return new Result<Adress>(ResultCode.OK, Map.ConvertRouteAsAdress(routePostal));
            }

            return new Result<Adress>(result.Status, result.Value);
        }

    }
}
