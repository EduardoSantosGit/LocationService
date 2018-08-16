using LocationService.Domain.Common;
using LocationService.Domain.Interfaces;
using LocationService.Domain.Models;
using LocationService.Infrastructure.Common;
using LocationService.Infrastructure.Services.Addresses;
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
        public readonly AddressesServiceScrap _addressesServiceScrap;

        public ClientMailApi(string baseUrl, TimeSpan timeout) : base(baseUrl, timeout)
        {
            _baseUrl = baseUrl ?? "http://www.buscacep.correios.com.br/";
            _apiUrl = "sistemas/buscacep/resultadoBuscaCepEndereco.cfm";

            Client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.186 Safari/537.36");
            Client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
            Client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9");

            _addressesServiceScrap = new AddressesServiceScrap();
            _scrapParser = new ScrapParser();
        }

        public async Task<Result<string>> PostSendAsync(string term)
        {
            var nvc = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("relaxation", term),
                new KeyValuePair<string, string>("tipoCEP", "ALL"),
                new KeyValuePair<string, string>("semelhante", "N")
            };

            var result = await this.PostFormUrlEncodedAsync(_apiUrl, nvc);

            return await ResultOperations.ReadHttpResult(result);
        }

        public async Task<Result<string>> PostSliceSendAsync(string term, int lines, int init, int last)
        {
            var nvc = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("relaxation", term),
                new KeyValuePair<string, string>("exata", "S"),
                new KeyValuePair<string, string>("semelhante", "N"),
                new KeyValuePair<string, string>("tipoCEP", "ALL"),
                new KeyValuePair<string, string>("qtdrow", lines.ToString()),
                new KeyValuePair<string, string>("pagini", init.ToString()),
                new KeyValuePair<string, string>("pagfim", last.ToString())
            };

            var result = await this.PostFormUrlEncodedAsync(_apiUrl, nvc);

            return await ResultOperations.ReadHttpResult(result);
        }

        public async Task<Result<Address>> GetAddressesZipCode(string zipCode)
        {
            var result = await PostSendAsync(zipCode);

            if(result.Status == ResultCode.OK)
            {
                if(_scrapParser.ContainsValue(result.ValueType, "<p>DADOS ENCONTRADOS COM SUCESSO.</p>", true) == true
                    || _scrapParser.ContainsValue(result.ValueType, "<p>RESULTADO SUPERIOR A ", true) == true)
                {
                    var retAddress = _addressesServiceScrap.GetAddressesPageCode(result.ValueType);

                    if(retAddress.Status == ResultCode.OK)
                        return new Result<Address>(ResultCode.OK, retAddress.ValueType);
                    else
                        return new Result<Address>(retAddress.Status, retAddress.Value);
                }
                else
                    return new Result<Address>(ResultCode.NotFound, "requested data not found");
            }

            return new Result<Address>(result.Status, result.Value);
        }

        public async Task<Result<List<Address>>> GetAddressesTerm(string term)
        {
            var result = await PostSendAsync(term);

            if(result.Status == ResultCode.OK)
            {
                if (_scrapParser.ContainsValue(result.ValueType, "<p>DADOS ENCONTRADOS COM SUCESSO.</p>", true) == true
                    || _scrapParser.ContainsValue(result.ValueType, "<p>RESULTADO SUPERIOR A ", true) == true)
                {
                    var retAddress = await SliceManagement(term, result.ValueType);

                    if (retAddress.Status == ResultCode.OK)
                        return new Result<List<Address>>(ResultCode.OK, retAddress.ValueType);
                    else
                        return new Result<List<Address>>(retAddress.Status, retAddress.Value);
                }
                else
                    return new Result<List<Address>>(ResultCode.NotFound, "requested data not found");
            }
           
            return new Result<List<Address>>(result.Status, result.Value);
        }

        public async Task<Result<List<Address>>> SliceManagement(string term, string html)
        {
            var count = _addressesServiceScrap.CountPagesTable(html);
            var listAddress = new List<Address>();

            var retAddress = _addressesServiceScrap.GetAddressesPageTerm(html);

            if (retAddress.Status != ResultCode.OK)
                return new Result<List<Address>>(retAddress.Status, retAddress.Value);

            listAddress.AddRange(retAddress.ValueType);

            if (count > 50)
            {
                var pointerInit = 50;
                var rest = count - 50;
                var lines = 0;
                var subtraction = 0;

                do
                {
                    if (rest >= 90)
                    {
                        lines = pointerInit + 90;
                        subtraction = 90;
                    }
                    else
                    {
                        lines = (pointerInit + rest);
                        subtraction = (pointerInit + rest);
                    }
                        
                    var retString = await PostSliceSendAsync(term, 50, pointerInit+1, lines);

                    if (retString.Status != ResultCode.OK)
                        return new Result<List<Address>>(retString.Status, retString.Value);

                    retAddress = _addressesServiceScrap.GetAddressesPageTerm(retString.ValueType);

                    if (retAddress.Status != ResultCode.OK)
                        return new Result<List<Address>>(retAddress.Status, retAddress.Value);

                    listAddress.AddRange(retAddress.ValueType);

                    pointerInit = lines;
                    rest = rest - subtraction;

                } while (count != pointerInit);
                

            }

            return new Result<List<Address>>(ResultCode.OK, listAddress);
        }

    }
}
