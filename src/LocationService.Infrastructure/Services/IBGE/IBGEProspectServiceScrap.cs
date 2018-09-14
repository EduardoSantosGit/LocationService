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

        public Result<string> VerifyPageComplete(string html)
        {
            var value = _scrapParser.ContainsValue(html, 
                "<app>Carregando...</app>", false);

            if (!value)
                return new Result<string>(ResultCode.OK);

            return new Result<string>(ResultCode.Error);
        }

        public Result<County> GetCountryPage(string html)
        {
            var country = new County();

            try
            {
                var retPop = GetPopulation(html);
                if (retPop.Status == ResultCode.OK)
                    country.Population = retPop.ValueType;

                var retWork = GetWorkIncome(html);
                if (retWork.Status == ResultCode.OK)
                    country.WorkIncome = retWork.ValueType;

                var retEdu = GetEducation(html);
                if (retEdu.Status == ResultCode.OK)
                    country.Education = retEdu.ValueType;

                var retEco = GetEconomy(html);
                if (retEco.Status == ResultCode.OK)
                    country.Economy = retEco.ValueType;
            }
            catch(Exception ex)
            {
                return new Result<County>(ResultCode.Error, ex.Message);
            }

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
                .ScrapBlockPage(html, "colspan=\"2\">Educação</th>", "colspan=\"2\">Economia</th>");

            var tablesEdu = blockPop.SplitString("<tr _ngcontent-c2017=");

            var education = new Education();

            try
            {
                if (!string.IsNullOrEmpty(tablesEdu[1]?.ToString()))
                    education.SchoolingRate = _scrapParser
                        .ScrapBlockPage(tablesEdu[1], "class=\"lista__valor\" colspan=\"2\">",
                                              "<span _ngcontent")?.Trim();

                if (!string.IsNullOrEmpty(tablesEdu[3]?.ToString()))
                    education.EarlyYearSchool = _scrapParser
                        .ScrapBlockPage(tablesEdu[3], "class=\"lista__valor\" colspan=\"2\">",
                                              "<span _ngcontent")?.Trim();

                if (!string.IsNullOrEmpty(tablesEdu[5]?.ToString()))
                    education.FinalYearSchool = _scrapParser
                        .ScrapBlockPage(tablesEdu[5], "class=\"lista__valor\" colspan=\"2\">",
                                              "<span _ngcontent")?.Trim();

                if (!string.IsNullOrEmpty(tablesEdu[7]?.ToString()))
                    education.EnrollSchoolFund = _scrapParser
                        .ScrapBlockPage(tablesEdu[7], "class=\"lista__valor\" colspan=\"2\">",
                                              "<span _ngcontent")?.Trim();

                if (!string.IsNullOrEmpty(tablesEdu[9]?.ToString()))
                    education.EnrollSchoolAvg = _scrapParser
                        .ScrapBlockPage(tablesEdu[9], "class=\"lista__valor\" colspan=\"2\">",
                                              "<span _ngcontent")?.Trim();

                if (!string.IsNullOrEmpty(tablesEdu[11]?.ToString()))
                    education.TeacherSchoolFund = _scrapParser
                        .ScrapBlockPage(tablesEdu[11], "class=\"lista__valor\" colspan=\"2\">",
                                              "<span _ngcontent")?.Trim();

                if (!string.IsNullOrEmpty(tablesEdu[13]?.ToString()))
                    education.TeacherSchoolAvg = _scrapParser
                        .ScrapBlockPage(tablesEdu[13], "class=\"lista__valor\" colspan=\"2\">",
                                              "<span _ngcontent")?.Trim();

                if (!string.IsNullOrEmpty(tablesEdu[15]?.ToString()))
                    education.InstituteSchoolFund = _scrapParser
                        .ScrapBlockPage(tablesEdu[15], "class=\"lista__valor\" colspan=\"2\">",
                                              "<span _ngcontent")?.Trim();
                
                if (!string.IsNullOrEmpty(tablesEdu[17]?.ToString()))
                    education.InstituteSchoolAvg = _scrapParser
                        .ScrapBlockPage(tablesEdu[17], "class=\"lista__valor\" colspan=\"2\">",
                                              "<span _ngcontent")?.Trim();
            }
            catch (Exception ex)
            {
                return new Result<Education>(ResultCode.Error, ex.Message);
            }

            return new Result<Education>(ResultCode.OK, education);
        }

        public Result<Economy> GetEconomy(string html)
        {
            var blockPop = _scrapParser
                .ScrapBlockPage(html, "colspan=\"2\">População</th>", "colspan=\"2\">Trabalho e Rendimento</th>");

            var tablesPop = blockPop.SplitString("<tr _ngcontent-c2017=");

            var economy = new Economy();

            try
            {
                if (!string.IsNullOrEmpty(tablesPop[1]?.ToString()))
                    economy.PIB = _scrapParser
                        .ScrapBlockPage(tablesPop[1], "class=\"lista__valor\" colspan=\"2\">",
                                              "<span _ngcontent")?.Trim();

                if (!string.IsNullOrEmpty(tablesPop[3]?.ToString()))
                    economy.PercRevFontExt = _scrapParser
                        .ScrapBlockPage(tablesPop[3], "class=\"lista__valor\" colspan=\"2\">",
                                              "<span _ngcontent")?.Trim();

                if (!string.IsNullOrEmpty(tablesPop[5]?.ToString()))
                    economy.IndDesenHumWor = _scrapParser
                        .ScrapBlockPage(tablesPop[5], "class=\"lista__valor\" colspan=\"2\">",
                                              "<span _ngcontent")?.Trim();

                if (!string.IsNullOrEmpty(tablesPop[7]?.ToString()))
                    economy.AmountRecFulfilled = _scrapParser
                        .ScrapBlockPage(tablesPop[7], "class=\"lista__valor\" colspan=\"2\">",
                                              "<span _ngcontent")?.Trim();

                if (!string.IsNullOrEmpty(tablesPop[9]?.ToString()))
                    economy.AmountComExp = _scrapParser
                        .ScrapBlockPage(tablesPop[9], "class=\"lista__valor\" colspan=\"2\">",
                                              "<span _ngcontent")?.Trim();
            }
            catch (Exception ex)
            {
                return new Result<Economy>(ResultCode.Error, ex.Message);
            }

            return new Result<Economy>(ResultCode.OK, economy);
        }

    }
}
