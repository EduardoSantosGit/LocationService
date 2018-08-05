using LocationService.Infrastructure.Services.Provider;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LocationService.IntegrationTest.Address
{
    public class AddressesClientApiIntegrationTest
    {
        public ClientMailApi CreateInstance()
        {
            return new ClientMailApi("http://www.buscacep.correios.com.br/", TimeSpan.FromMinutes(2));
        }

        [Fact]
        public async Task GetAddressesCep_WhenCepString_ReturnsPageNotNull()
        {
            var addressApi = CreateInstance();
            var result = await addressApi.PostSendAsync("01311200");
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetAddressesCep_WhenCepStringNotFound_ReturnsPageNotFound()
        {
            var addressApi = CreateInstance();
            var result = await addressApi.PostSendAsync("0000000000");

            var type = result.ValueType is string;
            var pageData = result.ValueType.Contains("DADOS NAO ENCONTRADOS");

            Assert.NotNull(result);
            Assert.True(type);
            Assert.True(pageData);
        }

        [Fact]
        public async Task GetAddressesCep_WhenCepStringCaracterInvalid_ReturnsPageNotFound()
        {
            var addressApi = CreateInstance();
            var result = await addressApi.PostSendAsync("01311300a");

            var type = result.ValueType is string;
            var pageData = result.ValueType.Contains("DADOS NAO ENCONTRADOS");

            Assert.NotNull(result);
            Assert.True(type);
            Assert.True(pageData);
        }

        [Fact]
        public async Task GetAddressesCep_WhenCepStringValid_ReturnsPage()
        {
            var addressApi = CreateInstance();
            var result = await addressApi.PostSendAsync("01311300");

            var type = result.ValueType is string;
            var pageData = result.ValueType.Contains("DADOS ENCONTRADOS COM SUCESSO.");

            Assert.NotNull(result);
            Assert.True(type);
            Assert.True(pageData);
        }

        [Fact]
        public async Task GetAddressesCep_WhenCepStringValidTrace_ReturnsPage()
        {
            var addressApi = CreateInstance();
            var result = await addressApi.PostSendAsync("01311-300");

            var type = result.ValueType is string;
            var pageData = result.ValueType.Contains("DADOS ENCONTRADOS COM SUCESSO.");

            Assert.NotNull(result);
            Assert.True(type);
            Assert.True(pageData);
        }

        [Fact]
        public async Task GetAddressesCep_WhenNameStreet_ReturnsPage()
        {
            var addressApi = CreateInstance();
            var result = await addressApi.PostSendAsync("Avenida Vital Brasil");

            var type = result.ValueType is string;
            var pageData = result.ValueType.Contains("DADOS ENCONTRADOS COM SUCESSO.");

            Assert.NotNull(result);
            Assert.True(type);
            Assert.True(pageData);
        }
    }
}
