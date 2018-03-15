using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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

        public async Task<string> PostFormUrlEncodedAsync(string url, List<KeyValuePair<string,string>> nvc)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                BaseUrl + url)
            { Content = new FormUrlEncodedContent(nvc) };

            var response = await Client.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return HttpUtility.HtmlDecode(response.Content.ReadAsStringAsync().Result);
            }

            return null;
        }

        public async Task<string> GetAsync(string url)
        {
            var response = await Client.GetAsync(url);

            if(response.StatusCode == HttpStatusCode.OK)
            {
                return HttpUtility.HtmlDecode(response.Content.ReadAsStringAsync().Result);
            }

            return null;
        }
    }
}
