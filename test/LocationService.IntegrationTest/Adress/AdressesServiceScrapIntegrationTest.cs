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
            Assert.Equal("Avenida Paulista - de 1047 a 1865 - lado ímpar", adress.Street);
            Assert.Equal("Bela Vista", adress.District);
            Assert.Equal("São Paulo/SP", adress.Locality);
            Assert.Equal("01311-200", adress.ZipCode);
        }
    }
}
