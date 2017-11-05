using LocationService.Infrastructure.Services.Adresses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LocationService.IntegrationTest.Adress
{
    public class AdressesServiceScrapIntegrationTest
    {
        public AdressesServiceScrap CreateInstance()
        {
            return new AdressesServiceScrap();
        }

        [Fact]
        public async Task GetAdressesPage_WhenHtmlData_ReturnsAdress()
        {
            var adressApi = new AdressesClientApi();
            var result = await adressApi.GetAdressesCep("01311200");

            var scrap = CreateInstance();
            var adress = scrap.GetAdressesPage(result);

            Assert.NotNull(adress);  
        }
    }
}
