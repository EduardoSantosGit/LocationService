using LocationService.Domain.Common;
using LocationService.Domain.Models;
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

        public int CountPagesTable(string initialHtml)
        {
            var blockQuantity = _scrapParser.ScrapBlockPage(initialHtml, "name=pagfim value=\"100\">", "<form name=\"Geral\" id=\"Geral\"");
            var quantity = _scrapParser.ScrapBlockPage(blockQuantity, "de", "<br><br>")?.Trim();
            var count = Convert.ToInt32(quantity);
            return count;
        }

        public Result<Address> GetAddressesPageCode(string html)
        {
            var address = default(Address);
            try
            {
                var table = _scrapParser.ScrapBlockPage(html, "<table class=\"tmptabela\">", "</table>");

                var lines = table.SplitString("<tr>");
                var columns = lines[1].SplitString("<td");

                address = new Address
                {
                    Street = _scrapParser.ScrapBlockPage(columns[1], "\">", "</td>")?.Trim(),
                    District = _scrapParser.ScrapBlockPage(columns[2], "\">", "</td>")?.Trim(),
                    Locality = _scrapParser.ScrapBlockPage(columns[3], "\">", "</td>")?.Trim(),
                    ZipCode = _scrapParser.ScrapBlockPage(columns[4], "\">", "</td>")?.Trim()
                };
            }
            catch (Exception ex)
            {
                return new Result<Address>(ResultCode.Error, ex.Message);
            }

            return new Result<Address>(ResultCode.OK, address);
        }

        public Result<List<Address>> GetAddressesPageTerm(string html)
        {
            var table = _scrapParser.ScrapBlockPage(html, "<table class=\"tmptabela\">", "</table>");
            var lines = table.SplitString("<tr");

            var listAddress = new List<Address>();
            var listAddressDouble = new List<Address>();

            try
            {
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
            }
            catch (Exception ex)
            {
                return new Result<List<Address>>(ResultCode.Error, ex.Message);
            }

            return new Result<List<Address>>(ResultCode.OK, listAddress);
        }
    }
}
