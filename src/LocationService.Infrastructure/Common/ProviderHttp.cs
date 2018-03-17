using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LocationService.Infrastructure.Common
{
    public class ProviderHttp
    {
        protected readonly HttpClient Client;
        protected readonly string BaseUrl;

        public ProviderHttp(string baseUrl, TimeSpan timeout)
        {
            Client = new HttpClient
            {
                Timeout = timeout
            };
        }

        public async Task<HttpResponseMessage> PostFormUrlEncodedAsync(string url, List<KeyValuePair<string,string>> nvc)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                BaseUrl + url)
            { Content = new FormUrlEncodedContent(nvc) };

            var response = await Client.SendAsync(request);

            return response;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            var response = await Client.GetAsync(url);
            return response;
        }
    }
}
