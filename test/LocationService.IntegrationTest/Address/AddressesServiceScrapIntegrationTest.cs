using LocationService.Infrastructure.Services.Addresses;
using LocationService.Infrastructure.Services.Provider;
using System;
using System.Threading.Tasks;
using Xunit;

namespace LocationService.IntegrationTest.Address
{
    public class AddressesServiceScrapIntegrationTest
    {
        public AddressesServiceScrap CreateInstance()
        {
            return new AddressesServiceScrap();
        }

        [Fact]
        public async Task GetAddressesPage_WhenHtmlData_ReturnsAddress()
        {
            var addressApi = new ClientMailApi("http://www.buscacep.correios.com.br/", TimeSpan.FromSeconds(30));
            var result = await addressApi.PostSendAsync("01311200");

            var scrap = CreateInstance();
            var address = scrap.GetAddressesPageCode(result.ValueType).ValueType;

            Assert.NotNull(address);
            Assert.Equal("Avenida Paulista - de 1047 a 1865 - lado ímpar", address.Street);
            Assert.Equal("Bela Vista", address.District);
            Assert.Equal("São Paulo/SP", address.Locality);
            Assert.Equal("01311-200", address.ZipCode);
        }

        [Fact]
        public async Task GetAddressesPage_WhenHtmlDataTerm_ReturnsListAddress()
        {
            var addressApi = new ClientMailApi("http://www.buscacep.correios.com.br/", TimeSpan.FromSeconds(30));
            var result = await addressApi.PostSendAsync("Avenida Vital Brasil");

            var scrap = CreateInstance();
            var address = scrap.GetAddressesPageTerm(result.ValueType).ValueType;

            Assert.NotNull(address);
            Assert.Equal(24, address.Count);
        }

        [Fact]
        public async Task GetAddressesPage_WhenHtmlDataTermZipcode_ReturnsListAddress()
        {
            var addressApi = new ClientMailApi("http://www.buscacep.correios.com.br/", TimeSpan.FromSeconds(30));
            var result = await addressApi.PostSendAsync("01311200");

            var scrap = CreateInstance();
            var address = scrap.GetAddressesPageTerm(result.ValueType).ValueType;

            Assert.NotNull(address);
            Assert.Equal(1, address.Count);
        }

        [Fact]
        public async Task GetAddressesPage_WhenHtmlDataTermAll_ReturnsListAddressLarger1000()
        {
            var addressApi = new ClientMailApi("http://www.buscacep.correios.com.br/", TimeSpan.FromSeconds(30));
            var result = await addressApi.GetAddressesTerm("Avenida Paulista");

            Assert.NotNull(result.ValueType);
            Assert.Equal(1144, result.ValueType.Count);
        }

    }
}
