using LocationService.Infrastructure.Services.Adresses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LocationService.IntegrationTest.Adress
{
    public class AdressesClientApiIntegrationTest
    {
        public AdressesClientApi CreateInstance()
        {
            return new AdressesClientApi();
        }

        [Fact]
        public async Task GetAdressesCep_WhenCepString_ReturnsPageNotNull()
        {
            var adressApi = CreateInstance();
            var result = await adressApi.GetAsync("01311200");
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetAdressesCep_WhenCepStringNotFound_ReturnsPageNotFound()
        {
            var adressApi = CreateInstance();
            var result = await adressApi.GetAsync("0000000000");

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
            var result = await adressApi.GetAsync("01311300a");

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
            var result = await adressApi.GetAsync("01311300");

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
            var result = await adressApi.GetAsync("01311-300");

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
            var result = await adressApi.GetAsync("Avenida Vital Brasil");

            var type = result is string;
            var pageData = result.Contains("DADOS ENCONTRADOS COM SUCESSO.");

            Assert.NotNull(result);
            Assert.True(type);
            Assert.True(pageData);
        }

    }
}
