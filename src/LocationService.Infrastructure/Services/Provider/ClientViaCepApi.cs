using LocationService.Domain.Common;
using LocationService.Domain.Interfaces;
using LocationService.Domain.Models;
using LocationService.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LocationService.Infrastructure.Services.Provider
{
    public class ClientViaCepApi : ProviderHttp, IAddressProvider
    {
        public readonly string _baseUrl;
        public readonly string _apiUrl;

        public ClientViaCepApi(string baseUrl, TimeSpan timeout) : base(baseUrl, timeout)
        {
            _baseUrl = "https://viacep.com.br/";
            _apiUrl = "ws/";
        }

        public async Task<Result<List<Adress>>> GetAdressesTerm(string term)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Adress>> GetAdressesZipCode(string zipCode)
        {
            var result = await this.GetAsync($@"{_baseUrl}{_apiUrl}{zipCode}/json");

            var resultString = await ResultOperations.ReadHttpResult(result);

            return new Result<Adress>(resultString.Status, resultString.Value);
        }

    }
}
