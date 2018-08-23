using LocationService.Domain.Common;
using LocationService.Domain.Models.IBGE;
using LocationService.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LocationService.Infrastructure.Services.Provider.Statistic
{
    public class ClientIBGEProspect : ProviderHttp
    {
        public readonly string _baseUrl;
        public readonly string _apiUrl;
        public readonly string _endpointLast;

        public ClientIBGEProspect(string baseUrl, TimeSpan timeout) : base(baseUrl, timeout)
        {
            _baseUrl = baseUrl ?? "https://cidades.ibge.gov.br/";
            _apiUrl = "brasil/";
            _endpointLast = "panorama";
        }

        public async Task<Result<County>> GetCountryByName(string uf, string state)
        {

            var retMessage = await this.GetAsync($"{_baseUrl}{_apiUrl}{uf}/{state}/{_endpointLast}");
            var result = await ResultOperations.ReadHttpResult(retMessage);

            return null;
        }


    }
}
