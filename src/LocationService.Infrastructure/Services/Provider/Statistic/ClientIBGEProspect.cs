using LocationService.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocationService.Infrastructure.Services.Provider.Statistic
{
    public class ClientIBGEProspect : ProviderHttp
    {
        public readonly string _baseUrl;
        public readonly string _apiUrl;

        public ClientIBGEProspect(string baseUrl, TimeSpan timeout) : base(baseUrl, timeout)
        {
            _baseUrl = baseUrl ?? "https://cidades.ibge.gov.br/";
            _apiUrl = "brasil/";
        }

        


    }
}
