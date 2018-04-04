using LocationService.Domain.Interfaces;
using LocationService.Domain.Models;
using LocationService.Infrastructure.Common;
using LocationService.Infrastructure.Services.Adresses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LocationService.Infrastructure.Services.Provider
{
    public class ClientMailApi : ProviderHttp, IAddressProvider
    {
        public readonly string _baseUrl;
        public readonly string _apiUrl;
        public readonly ScrapParser _scrapParser;
        public readonly AdressesServiceScrap _adressesServiceScrap;

        public ClientMailApi(string baseUrl, TimeSpan timeout) : base(baseUrl, timeout)
        {
            _baseUrl = baseUrl ?? "http://www.buscacep.correios.com.br/";
            _apiUrl = "sistemas/buscacep/resultadoBuscaCepEndereco.cfm";

            Client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.186 Safari/537.36");
            Client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
            Client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9");

            _adressesServiceScrap = new AdressesServiceScrap();
            _scrapParser = new ScrapParser();
        }

        public async Task<string> PostSendAsync(string term)
        {
            var nvc = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("relaxation", term),
                new KeyValuePair<string, string>("tipoCEP", "ALL"),
                new KeyValuePair<string, string>("semelhante", "N")
            };

            var result = await this.PostFormUrlEncodedAsync(_apiUrl, nvc);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var e = result.Content.ReadAsStringAsync().Result;
                return HttpUtility.HtmlDecode(result.Content.ReadAsStringAsync().Result);
            }

            return null;
        }

        public async Task<Adress> GetAdressesZipCode(string zipCode)
        {
            var html = await PostSendAsync(zipCode);

            var valid = _scrapParser.ContainsValue(html, "<p>DADOS ENCONTRADOS COM SUCESSO.</p>", true);

            if (valid)
            {
                return _adressesServiceScrap.GetAdressesPageCode(html);
            }

            return null;
        }

        public async Task<List<Adress>> GetAdressesTerm(string term)
        {

            var html = await PostSendAsync(term);

            var valid = _scrapParser.ContainsValue(html, "<p>DADOS ENCONTRADOS COM SUCESSO.</p>", true);

            if (valid)
            {
                return _adressesServiceScrap.GetAdressesPageTerm(html);
            }

            return null;
        }
    }
}
