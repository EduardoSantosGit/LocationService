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
            var result = await adressApi.GetAdressesCep("01311200");
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetAdressesCep_WhenCepStringNotFound_ReturnsPageNotFound()
        {
            var adressApi = CreateInstance();
            var result = await adressApi.GetAdressesCep("0000000000");

            var type = result is string;
            var pageData = result.Contains("DADOS NAO ENCONTRADOS");

            Assert.NotNull(result);
            Assert.True(type);
            Assert.True(pageData);
        }


    }
}
