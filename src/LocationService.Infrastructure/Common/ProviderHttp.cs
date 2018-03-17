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
        protected readonly HttpRequestHeaders Headers;

        public ProviderHttp(string baseUrl, TimeSpan timeout)
        {
            Client = new HttpClient
            {
                Timeout = timeout
            };

            Client.DefaultRequestHeaders.Add("Connection", "keep-alive");
            Client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.186 Safari/537.36");
            Client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
            Client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
            Client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9");
        }

        public async Task<HttpResponseMessage> PostFormUrlEncodedAsync(string url, List<KeyValuePair<string,string>> nvc)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                BaseUrl + url)
            { Content = new FormUrlEncodedContent(nvc) };

            var response = await Client.SendAsync(request);

            return response;
            /*if (response.StatusCode == HttpStatusCode.OK)
            {
                return HttpUtility.HtmlDecode(response.Content.ReadAsStringAsync().Result);
            }

            return null;*/
        }

        public async Task<string> GetAsync(string url)
        {
            Client.DefaultRequestHeaders = new System.Net.Http.Headers.HttpRequestHeaders();

            var response = await Client.GetAsync(url);
            return response;
        }
    }
}
