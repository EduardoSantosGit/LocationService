using LocationService.Infrastructure.Services.Addresses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace LocationService.Test.Address
{
    public class AddressesServiceScrapTest
    {
        
        [Fact]
        public void GetAddressesPageTerm_WhenStreet_ReturnsValue()
        {
            var html = File.ReadAllText(@"../../../Files/HTML/AvenidaVitalBrasil.html");

            var addressScrap = new AddressesServiceScrap();

            var list = addressScrap.GetAddressesPageTerm(html);

            Assert.NotNull(list);
            Assert.Equal(24, list.Count);
        }

        [Fact]
        public void GetaddressesPageCode_WhenZipCode_ReturnsValue()
        {
            var html = File.ReadAllText(@"../../../Files/HTML/01311200.html");

            var addressScrap = new AddressesServiceScrap();

            var address = addressScrap.GetAddressesPageCode(html);

            Assert.NotNull(address);
            Assert.Equal("Avenida Paulista - de 1047 a 1865 - lado ímpar", address.Street);
            Assert.Equal("Bela Vista", address.District);
            Assert.Equal("São Paulo/SP", address.Locality);
            Assert.Equal("01311-200", address.ZipCode);
        }

    }
}
