using LocationService.Domain.Interfaces;
using LocationService.Domain.Models;
using LocationService.Infrastructure.Common;
using LocationService.Infrastructure.Services.Provider;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LocationService.Infrastructure.Services.Adresses
{
    public class AdressesMailService : IAddressProvider
    {
        public readonly ClientMailApi _adressesClientApi;
        public readonly AdressesServiceScrap _adressesServiceScrap;
        public readonly ScrapParser _scrapParser;

        public AdressesMailService(ClientMailApi adressesClientApi)
        {
            _adressesClientApi = adressesClientApi;
            _adressesServiceScrap = new AdressesServiceScrap();
            _scrapParser = new ScrapParser();
        }

        public async Task<Adress> GetAdressesZipCode(string zipCode)
        {
            var html = await _adressesClientApi.PostSendAsync(zipCode);

            var valid = _scrapParser.ContainsValue(html, "<p>DADOS ENCONTRADOS COM SUCESSO.</p>", true);

            if (valid)
            {
                return _adressesServiceScrap.GetAdressesPageCode(html);
            }
            
            return null;
        }

        public async Task<List<Adress>> GetAdressesTerm(string term)
        {

            var html = await _adressesClientApi.PostSendAsync(term);

            var valid = _scrapParser.ContainsValue(html, "<p>DADOS ENCONTRADOS COM SUCESSO.</p>", true);

            if (valid)
            {
                return _adressesServiceScrap.GetAdressesPageTerm(html);
            }

            return null;
        }

    }
}
