using LocationService.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LocationService.Infrastructure.Services.Provider
{
    public class ClientMailApi : ProviderHttp
    {
        public readonly string _baseUrl;
        public readonly string _apiUrl;

        public ClientMailApi(string baseUrl, TimeSpan timeout) : base(baseUrl, timeout)
        {
            _baseUrl = "http://www.buscacep.correios.com.br/";
            _apiUrl = "sistemas/buscacep/resultadoBuscaCepEndereco.cfm";

        }

        public async Task<string> GetAsync(string term)
        {
            var nvc = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("relaxation", term),
                new KeyValuePair<string, string>("tipoCEP", "ALL"),
                new KeyValuePair<string, string>("semelhante", "N")
            };

            var result = await this.PostFormUrlEncodedAsync(_apiUrl, nvc);

            return null;
        }
    }
}
