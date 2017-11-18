﻿using LocationService.Domain.Models;
using LocationService.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LocationService.Infrastructure.Services.Adresses
{
    public class AdressesServiceScrap
    {
        private readonly ScrapParser _scrapParser;

        public AdressesServiceScrap()
        {
            _scrapParser = new ScrapParser();
        }

        public Adress GetAdressesPage(string html)
        {
            var table = _scrapParser.ScrapBlockPage(html, "<table class=\"tmptabela\">", "</table>");
            var lines = table.Split("<tr>");
            var columns = lines[1].Split("<td");

            var adress = new Adress
            {
                Street = _scrapParser.ScrapBlockPage(columns[1], "\">", "</td>")?.Trim(),
                District = _scrapParser.ScrapBlockPage(columns[2], "\">", "</td>")?.Trim(),
                Locality = _scrapParser.ScrapBlockPage(columns[3], "\">", "</td>")?.Trim(),
                ZipCode = _scrapParser.ScrapBlockPage(columns[4], "\">", "</td>")?.Trim()
            };

            return adress;
        }
    }
}
