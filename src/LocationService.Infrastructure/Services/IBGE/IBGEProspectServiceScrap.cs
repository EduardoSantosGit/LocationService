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

            var retPop = GetPopulation(html);
            if (retPop.Status == ResultCode.OK)
                country.Population = retPop.ValueType;

            var retWork = GetWorkIncome(html);
            if (retWork.Status == ResultCode.OK)
                country.WorkIncome = retWork.ValueType;

            var retEdu = GetEducation(html);
            if (retEdu.Status == ResultCode.OK)
                country.Education = retEdu.ValueType;

            return new Result<County>(ResultCode.OK, country);
        }

        public Result<Population> GetPopulation(string html)
        {
            var blockPop = _scrapParser
                .ScrapBlockPage(html, "colspan=\"2\">População</th>", "colspan=\"2\">Trabalho e Rendimento</th>");

            var tablesPop = blockPop.SplitString("<tr _ngcontent-c2017=");

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

        public Result<WorkIncome> GetWorkIncome(string html)
        {
            var blockPop = _scrapParser
                .ScrapBlockPage(html, "colspan=\"2\">Trabalho e Rendimento</th>", "colspan=\"2\">Educação</th>");

            var tableWork = blockPop.SplitString("<tr _ngcontent-c2017=");

            var workInc = new WorkIncome();

            try
            {
                if (!string.IsNullOrEmpty(tableWork[1]?.ToString()))
                    workInc.SalaryAverageMonth = _scrapParser
                        .ScrapBlockPage(tableWork[1], "class=\"lista__valor\" colspan=\"2\">",
                                              "<span _ngcontent")?.Trim();

                if (!string.IsNullOrEmpty(tableWork[3]?.ToString()))
                    workInc.PeopleBusy = _scrapParser
                        .ScrapBlockPage(tableWork[3], "class=\"lista__valor\" colspan=\"2\">",
                                              "<span _ngcontent")?.Trim();

                if (!string.IsNullOrEmpty(tableWork[5]?.ToString()))
                    workInc.PopulationBusyPercentage = _scrapParser
                        .ScrapBlockPage(tableWork[5], "class=\"lista__valor\" colspan=\"2\">",
                                              "<span _ngcontent")?.Trim();

                if (!string.IsNullOrEmpty(tableWork[7]?.ToString()))
                    workInc.PopMonthMinWage = _scrapParser
                        .ScrapBlockPage(tableWork[7], "class=\"lista__valor\" colspan=\"2\">",
                                              "<span _ngcontent")?.Trim();
            }
            catch (Exception ex)
            {
                return new Result<WorkIncome>(ResultCode.Error, ex.Message);
            }

            return new Result<WorkIncome>(ResultCode.OK, workInc);
        }

        public Result<Education> GetEducation(string html)
        {
            var blockPop = _scrapParser
                .ScrapBlockPage(html, "colspan=\"2\">População</th>", "colspan=\"2\">Educação</th>");

            var tablesEdu = blockPop.SplitString("<tr _ngcontent-c2017=");

            var education = new Education();

            try
            {
                
            }
            catch (Exception ex)
            {
                return new Result<Education>(ResultCode.Error, ex.Message);
            }

            return new Result<Education>(ResultCode.OK, education);
        }

    }
}
