using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace LocationService.Infrastructure.Common
{
    public class ProviderHttp
    {

        protected readonly HttpClient _httpClient;
 
        public ProviderHttp(string baseUrl, TimeSpan timeout)
        {

            _httpClient.Timeout = timeout;

        }

    }
}
