using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LocationService.Infrastructure.Services.Adress
{
    public class AdressesClientApi
    {
        public readonly HttpClient _httpClient;
        public readonly string baseUrl;
        public readonly string apiUrl;

        public AdressesClientApi(HttpClient httpClient = null)
        {
            _httpClient = httpClient ?? new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        public async Task<string> GetAdressesCep(string zipCode)
        {

            var nvc = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("relaxation", zipCode),
                new KeyValuePair<string, string>("tipoCEP", "ALL"),
                new KeyValuePair<string, string>("semelhante", "N")
            };

            var request = new HttpRequestMessage(HttpMethod.Post,
                baseUrl + apiUrl) { Content = new FormUrlEncodedContent(nvc) };

            var response = await _httpClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Content.ReadAsStringAsync().Result;
            }

            return null;
        }
    }
}
