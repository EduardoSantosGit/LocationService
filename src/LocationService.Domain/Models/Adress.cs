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
    }
}
