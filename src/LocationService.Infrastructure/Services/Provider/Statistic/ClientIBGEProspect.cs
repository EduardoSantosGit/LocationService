using LocationService.Domain.Common;
using LocationService.Domain.Models.IBGE;
using LocationService.Infrastructure.Common;
using LocationService.Infrastructure.Services.IBGE;
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
        public readonly IbgeProspectServiceScrap _serviceScrap;

        public ClientIBGEProspect(string baseUrl, TimeSpan timeout) : base(baseUrl, timeout)
        {
            _baseUrl = baseUrl ?? "https://cidades.ibge.gov.br/";
            _apiUrl = "brasil/";
            _endpointLast = "panorama";
            _serviceScrap = new IbgeProspectServiceScrap();
        }

        public async Task<Result<County>> GetCountryByName(string uf, string state)
        {

            var retMessage = await this.GetAsync($"{_baseUrl}{_apiUrl}{uf}/{state}/{_endpointLast}");
            var result = await ResultOperations.ReadHttpResult(retMessage);

            if(result.Status == ResultCode.OK)
            {
                var retCountry = _serviceScrap.GetCountryPage(result.ValueType);

                if (retCountry.Status == ResultCode.OK)
                    return new Result<County>(retCountry.Status, retCountry.ValueType);
            }

            return null;
        }

        

    }
}
