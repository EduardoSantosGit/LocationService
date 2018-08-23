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
            return new ClientIBGEProspect("http://www.buscacep.correios.com.br/", TimeSpan.FromMinutes(2));
        }

        [Fact]
        public async Task GetCountryByName_WhenNameState_ReturnsPageProspect()
        {
            var addressApi = CreateInstance();
            var result = await addressApi.GetCountryByName("am", "manaus");
            Assert.NotNull(result);
        }

    }
}
