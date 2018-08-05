using LocationService.Infrastructure.Services.Provider;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LocationService.IntegrationTest.Address
{
    public class AddressesProviderRoutePostalTest
    {

        //[Fact]
        //public async Task GetAdressesViaCep_WhenCepString_ReturnsJson()
        //{
        //    var adressApi = new ClientViaCepApi("https://viacep.com.br/", TimeSpan.FromSeconds(30));
        //    var result = await adressApi.GetAdressesZipCode("01311200");

        //    Assert.NotNull(result);
        //}

        //[Fact]
        //public async Task GetAdressesViaCep_WhenCepString_ReturnsDataJson()
        //{
        //    var adressApi = new ClientViaCepApi("https://viacep.com.br/", TimeSpan.FromSeconds(30));
        //    var resultAdress = await adressApi.GetAdressesZipCode("01311200");

        //    var result = resultAdress.ValueType;

        //    Assert.NotNull(result);
        //    Assert.True(result.Contains("cep"));
        //    Assert.True(result.Contains("logradouro"));
        //    Assert.True(result.Contains("complemento"));
        //    Assert.True(result.Contains("bairro"));
        //    Assert.True(result.Contains("localidade"));
        //    Assert.True(result.Contains("uf"));
        //    Assert.True(result.Contains("unidade"));
        //    Assert.True(result.Contains("ibge"));
        //    Assert.True(result.Contains("gia"));
        //}

        //[Fact]
        //public async Task GetAdressesViaCep_WhenCepString_ReturnsDataJsonCorrect()
        //{
        //    var adressApi = new ClientViaCepApi("https://viacep.com.br/", TimeSpan.FromSeconds(30));
        //    var result = await adressApi.GetAdressesZipCode("01311200");

        //    Assert.NotNull(result);
        //    Assert.True(result.Contains("01311-200"));
        //    Assert.True(result.Contains("Avenida Paulista"));
        //    Assert.True(result.Contains("Bela Vista"));
        //    Assert.True(result.Contains("SP"));
        //    Assert.True(result.Contains("3550308"));
        //}
    }
}
