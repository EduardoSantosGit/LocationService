using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Reflection;
using System.Xml.Serialization;
using System.IO;

namespace LocationService.Infrastructure.Common
{
    public static class StringExtensions
    {
        public static string[] SplitString(this string value, string separating)
        {
            string[] array = { separating };
            return value.Split(array, System.StringSplitOptions.RemoveEmptyEntries);
        }
        
    }
}
