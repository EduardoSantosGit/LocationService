using System;
using System.Collections.Generic;
using System.Text;

namespace LocationService.Domain.Models
{
    public class Adress
    {
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public string Locality { get; set; }
        public string Adjunct { get; set; }
        public string UF { get; set; }
        public string Unit { get; set; }
        public string CodeIbge { get; set; }
        public string Gia { get; set; }
    }
}
