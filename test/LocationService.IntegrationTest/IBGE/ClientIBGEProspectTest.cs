using LocationService.Domain.Common;
using LocationService.Infrastructure.Services.Provider.Statistic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LocationService.IntegrationTest.IBGE
{
    public class ClientIBGEProspectTest
    {

        public ClientIBGEProspect CreateInstance()
        {
            return new ClientIBGEProspect("https://cidades.ibge.gov.br/", TimeSpan.FromMinutes(2));
        }

        [Fact]
        public async Task GetCountryByName_WhenNameState_ReturnsPageProspect()
        {
            var addressApi = CreateInstance();
            var result = await addressApi.GetCountryByName("am", "manaus");

            Assert.NotNull(result);
            Assert.Equal(ResultCode.OK, result.Status);
        }

        [Fact]
        public async Task GetCountryByName_WhenNameStateNotFound_ReturnsPageNotFound()
        {
            var addressApi = new ClientIBGEProspect("https://cidades.ibge.gov.br/", TimeSpan.FromMinutes(2));
            var result = await addressApi.GetCountryByName("am", "manas");
        
            Assert.NotNull(result);
            Assert.Equal(ResultCode.NotFound, result.Status);
        }

        [Fact]
        public async Task GetCountryByName_WhenUfEmpty_ReturnsPageBadRequest()
        {
            var addressApi = CreateInstance();
            var result = await addressApi.GetCountryByName("", "manas");

            Assert.NotNull(result);
            Assert.Equal(ResultCode.BadRequest, result.Status);
        }
    }
}
