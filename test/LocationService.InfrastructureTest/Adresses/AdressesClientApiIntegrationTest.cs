using LocationService.Infrastructure.Services.Adress;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LocationService.IntegrationTest.Adresses
{
    public class AdressesClientApiIntegrationTest
    {

        public AdressesClientApi CreateInstance()
        {
            return new AdressesClientApi();
        }

        public async Task GetAdressesCep_WhenCepString_ReturnsStringPageOk()
        {
            var adressApi = CreateInstance();
            var result = await adressApi.GetAdressesCep("01311200");

        }
    }
}
