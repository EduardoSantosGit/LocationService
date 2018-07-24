using LocationService.Infrastructure.Services.Provider;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LocationService.IntegrationTest.Adress
{
    public class AdressesProviderViaCepTest
    {

        [Fact]
        public async Task GetAdressesViaCep_WhenCepString_ReturnsPage()
        {
            var adressApi = new ClientViaCepApi("https://viacep.com.br/", TimeSpan.FromSeconds(30));
            var result = await adressApi.GetAsyncZipCode("01311200");
            Assert.NotNull(result);
        }

    }
}
