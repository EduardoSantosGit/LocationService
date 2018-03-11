using LocationService.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocationService.Infrastructure.Services.Provider
{
    public class ClientViaCepApi : ProviderHttp
    {
        public readonly string _baseUrl;
        public readonly string _apiUrl;

        public ClientViaCepApi(string baseUrl, TimeSpan timeout) : base(baseUrl, timeout)
        {

            _baseUrl = "https://viacep.com.br/";
            _apiUrl = "";

        }

    }
}
