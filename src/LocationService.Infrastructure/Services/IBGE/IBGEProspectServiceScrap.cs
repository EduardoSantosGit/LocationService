using LocationService.Domain.Common;
using LocationService.Domain.Models.IBGE;
using LocationService.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocationService.Infrastructure.Services.IBGE
{
    public class IbgeProspectServiceScrap
    {
        private readonly ScrapParser _scrapParser;

        public IbgeProspectServiceScrap()
        {
            _scrapParser = new ScrapParser();
        }

        public Result<County> GetCountryPage(string html)
        {
            var country = new County();

            var blockPop = _scrapParser
                .ScrapBlockPage(html, "colspan=\"2\">População</th>", "colspan=\"2\">Trabalho e Rendimento</th>");

            var retPop = GetPopulation(blockPop);
            if (retPop.Status == ResultCode.OK)
                country.Population = retPop.ValueType;

            return new Result<County>(ResultCode.OK, country);
        }

        public Result<Population> GetPopulation(string html)
        {
            var tablesPop = html.SplitString("<tr _ngcontent-c2017=");

            var population = new Population();

            try
            {
                if (!string.IsNullOrEmpty(tablesPop[1]?.ToString()))
                    population.PopulationEstimated = _scrapParser
                        .ScrapBlockPage(tablesPop[1], "class=\"lista__valor\" colspan=\"2\">",
                                              "<span _ngcontent")?.Trim();

                if (!string.IsNullOrEmpty(tablesPop[3]?.ToString()))
                    population.PopulationLastCensus = _scrapParser
                        .ScrapBlockPage(tablesPop[3], "class=\"lista__valor\" colspan=\"2\">",
                                              "<span _ngcontent")?.Trim();

                if (!string.IsNullOrEmpty(tablesPop[5]?.ToString()))
                    population.DemographicDensity = _scrapParser
                        .ScrapBlockPage(tablesPop[5], "class=\"lista__valor\" colspan=\"2\">",
                                              "<span _ngcontent")?.Trim();
            }
            catch(Exception ex)
            {
                return new Result<Population>(ResultCode.Error, ex.Message);
            }

            return new Result<Population>(ResultCode.OK, population);
        }



    }
}
