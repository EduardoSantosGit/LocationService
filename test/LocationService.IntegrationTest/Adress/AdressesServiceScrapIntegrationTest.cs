using LocationService.Infrastructure.Services.Adresses;
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
            var result = await adressApi.GetAsync("01311200");

            var scrap = CreateInstance();
            var adress = scrap.GetAdressesPageCode(result);

            Assert.NotNull(adress);
            Assert.Equal("Avenida Paulista - de 1047 a 1865 - lado ímpar", adress.Street);
            Assert.Equal("Bela Vista", adress.District);
            Assert.Equal("São Paulo/SP", adress.Locality);
            Assert.Equal("01311-200", adress.ZipCode);
        }

        [Fact]
        public async Task GetAdressesPage_WhenHtmlDataTerm_ReturnsListAdress()
        {
            var adressApi = new AdressesClientApi();
            var result = await adressApi.GetAsync("Avenida Vital Brasil");

            var scrap = CreateInstance();
            var adress = scrap.GetAdressesPageTerm(result);

            Assert.NotNull(adress);
            Assert.Equal(24, adress.Count);
        }

        [Fact]
        public async Task GetAdressesPage_WhenHtmlDataTermZipcode_ReturnsListAdress()
        {
            var adressApi = new AdressesClientApi();
            var result = await adressApi.GetAsync("01311200");

            var scrap = CreateInstance();
            var adress = scrap.GetAdressesPageTerm(result);

            Assert.NotNull(adress);
            Assert.Equal(24, adress.Count);
        }

    }
}
