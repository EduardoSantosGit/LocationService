using LocationService.Infrastructure.Services.Adresses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace LocationService.Test.Adress
{
    public class AdressesServiceScrapTest
    {
        
        [Fact]
        public void GetAdressesPageTerm_WhenStreet_ReturnsValue()
        {
            var html = File.ReadAllText(@"../../../Files/HTML/AvenidaVitalBrasil.html");

            var adressScrap = new AdressesServiceScrap();

            var list = adressScrap.GetAdressesPageTerm(html);

            Assert.NotNull(list);
            Assert.Equal(24, list.Count);
        }

        [Fact]
        public void GetAdressesPageCode_WhenZipCode_ReturnsValue()
        {
            var html = File.ReadAllText(@"../../../Files/HTML/01311200.html");

            var adressScrap = new AdressesServiceScrap();

            var adress = adressScrap.GetAdressesPageCode(html);

            Assert.NotNull(adress);
            Assert.Equal("Avenida Paulista - de 1047 a 1865 - lado ímpar", adress.Street);
            Assert.Equal("Bela Vista", adress.District);
            Assert.Equal("São Paulo/SP", adress.Locality);
            Assert.Equal("01311-200", adress.ZipCode);
        }

    }
}
