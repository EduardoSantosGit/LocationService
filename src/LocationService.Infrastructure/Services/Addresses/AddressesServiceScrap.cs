﻿using LocationService.Domain.Models;
using LocationService.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LocationService.Infrastructure.Services.Addresses
{
    public class AddressesServiceScrap
    {
        private readonly ScrapParser _scrapParser;

        public AddressesServiceScrap()
        {
            _scrapParser = new ScrapParser();
        }

        public Address GetAddressesPageCode(string html)
        {
            var table = _scrapParser.ScrapBlockPage(html, "<table class=\"tmptabela\">", "</table>");

            var lines = table.SplitString("<tr>");
            var columns = lines[1].SplitString("<td");

            var adress = new Address
            {
                Street = _scrapParser.ScrapBlockPage(columns[1], "\">", "</td>")?.Trim(),
                District = _scrapParser.ScrapBlockPage(columns[2], "\">", "</td>")?.Trim(),
                Locality = _scrapParser.ScrapBlockPage(columns[3], "\">", "</td>")?.Trim(),
                ZipCode = _scrapParser.ScrapBlockPage(columns[4], "\">", "</td>")?.Trim()
            };

            return adress;
        }

        public List<Address> GetAddressesPageTerm(string html)
        {
            var table = _scrapParser.ScrapBlockPage(html, "<table class=\"tmptabela\">", "</table>");
            var lines = table.SplitString("<tr");

            var listAddress = new List<Address>();
            var listAddressDouble = new List<Address>();

            for (var i = 2; i < lines.Length; i++)
            {
                var columns = lines[i].SplitString("<td");

                var col = _scrapParser.ScrapBlockPage(columns[1], "\">", "</td>")?.Trim();

                if (col.Contains("<br"))
                {
                    var duplicate = col.SplitString("<br>");

                    for (var j = 0; j < duplicate.Length; j++)
                    {
                        if (!string.IsNullOrEmpty(duplicate[j]))
                        {
                            var street = default(string);
                            if (duplicate[j].Contains("</a>") || duplicate[j].Contains(">"))
                            {
                                street = duplicate[j]?.Replace("</a>", "")?.Trim();
                            }
                            else
                            {
                                street = duplicate[j]?.Trim();
                            }

                            listAddress.Add(new Address
                            {
                                Street = street,
                                District = _scrapParser.ScrapBlockPage(columns[2], "\">", "</td>")?.Trim(),
                                Locality = _scrapParser.ScrapBlockPage(columns[3], "\">", "</td>")?.Trim(),
                                ZipCode = _scrapParser.ScrapBlockPage(columns[4], "\">", "</td>")?.Trim()
                            });
                        }
                    }
                }
                else
                {
                    var adress = new Address
                    {
                        Street = _scrapParser.ScrapBlockPage(columns[1], "\">", "</td>")?.Trim(),
                        District = _scrapParser.ScrapBlockPage(columns[2], "\">", "</td>")?.Trim(),
                        Locality = _scrapParser.ScrapBlockPage(columns[3], "\">", "</td>")?.Trim(),
                        ZipCode = _scrapParser.ScrapBlockPage(columns[4], "\">", "</td>")?.Trim()
                    };

                    listAddress.Add(adress);
                }
                
            }

            return listAddress;
        }
    }
}