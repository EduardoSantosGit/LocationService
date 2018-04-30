using LocationService.Infrastructure.Services.Adresses;
using LocationService.Infrastructure.Services.Provider;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LocationService.IntegrationTest.Adress
{
    public class AdressesClientApiIntegrationTest
    {
        public ClientMailApi CreateInstance()
        {
            return new ClientMailApi("http://www.buscacep.correios.com.br/", TimeSpan.FromMinutes(2));
        }

        [Fact]
        public async Task GetAdressesCep_WhenCepString_ReturnsPageNotNull()
        {
            var adressApi = CreateInstance();
            var result = await adressApi.PostSendAsync("01311200");
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetAdressesCep_WhenCepStringNotFound_ReturnsPageNotFound()
        {
            var adressApi = CreateInstance();
            var result = await adressApi.PostSendAsync("0000000000");

            var type = result is string;
            var pageData = result.Contains("DADOS NAO ENCONTRADOS");

            Assert.NotNull(result);
            Assert.True(type);
            Assert.True(pageData);
        }

        [Fact]
        public async Task GetAdressesCep_WhenCepStringCaracterInvalid_ReturnsPageNotFound()
        {
            var adressApi = CreateInstance();
            var result = await adressApi.PostSendAsync("01311300a");

            var type = result is string;
            var pageData = result.Contains("DADOS NAO ENCONTRADOS");

            Assert.NotNull(result);
            Assert.True(type);
            Assert.True(pageData);
        }

        [Fact]
        public async Task GetAdressesCep_WhenCepStringValid_ReturnsPage()
        {
            var adressApi = CreateInstance();
            var result = await adressApi.PostSendAsync("01311300");

            var type = result is string;
            var pageData = result.Contains("DADOS ENCONTRADOS COM SUCESSO.");

            Assert.NotNull(result);
            Assert.True(type);
            Assert.True(pageData);
        }

        [Fact]
        public async Task GetAdressesCep_WhenCepStringValidTrace_ReturnsPage()
        {
            var adressApi = CreateInstance();
            var result = await adressApi.PostSendAsync("01311-300");

            var type = result is string;
            var pageData = result.Contains("DADOS ENCONTRADOS COM SUCESSO.");

            Assert.NotNull(result);
            Assert.True(type);
            Assert.True(pageData);
        }

        [Fact]
        public async Task GetAdressesCep_WhenNameStreet_ReturnsPage()
        {
            var adressApi = CreateInstance();
            var result = await adressApi.PostSendAsync("Avenida Vital Brasil");

            var type = result is string;
            var pageData = result.Contains("DADOS ENCONTRADOS COM SUCESSO.");

            Assert.NotNull(result);
            Assert.True(type);
            Assert.True(pageData);
        }
    }
}
