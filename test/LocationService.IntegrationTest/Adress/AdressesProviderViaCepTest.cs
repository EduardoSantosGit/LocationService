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
        public async Task GetAdressesViaCep_WhenCepString_ReturnsJson()
        {
            var adressApi = new ClientViaCepApi("https://viacep.com.br/", TimeSpan.FromSeconds(30));
            var result = await adressApi.GetAsyncZipCode("01311200");

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetAdressesViaCep_WhenCepString_ReturnsDataJson()
        {
            var adressApi = new ClientViaCepApi("https://viacep.com.br/", TimeSpan.FromSeconds(30));
            var result = await adressApi.GetAsyncZipCode("01311200");

            Assert.NotNull(result);
            Assert.True(result.Contains("cep"));
            Assert.True(result.Contains("logradouro"));
            Assert.True(result.Contains("complemento"));
            Assert.True(result.Contains("bairro"));
            Assert.True(result.Contains("localidade"));
            Assert.True(result.Contains("uf"));
            Assert.True(result.Contains("unidade"));
            Assert.True(result.Contains("ibge"));
            Assert.True(result.Contains("gia"));
        }

    }
}
