using LocationService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocationService.Infrastructure.Utils
{
    public class Map
    {

        public static Address ConvertRouteAsAdress(AddressRoutePostal adressRoutePostal)
        {
            var adress = new Address
            {
                ZipCode = adressRoutePostal.Cep,
                Adjunct = adressRoutePostal.Complemento,
                CodeIbge = adressRoutePostal.Ibge,
                District = adressRoutePostal.Bairro,
                Gia = adressRoutePostal.Gia,
                Locality = adressRoutePostal.Localidade,
                Street = adressRoutePostal.Logradouro,
                UF = adressRoutePostal.UF,
                Unit = adressRoutePostal.Unidade
            };

            return adress;
        }

    }
}
