using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LocationService.Infrastructure.Services.Adresses
{
    public class AdressesClientApi
    {
        public readonly HttpClient _httpClient;
        public readonly string _baseUrl;
        public readonly string _apiUrl;

        public AdressesClientApi(HttpClient httpClient = null)
        {
            _httpClient = httpClient ?? new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
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

            var request = new HttpRequestMessage(HttpMethod.Post,
                _baseUrl + _apiUrl) { Content = new FormUrlEncodedContent(nvc) };

            var response = await _httpClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return HttpUtility.HtmlDecode(response.Content.ReadAsStringAsync().Result);
            }

            return null;
        }
    }
}
