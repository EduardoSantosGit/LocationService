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
            var blockPop = _scrapParser
                .ScrapBlockPage(html, "colspan=\"2\">População</th>", "colspan=\"2\">Trabalho e Rendimento</th>");

            return null;
        }
        

    }
}
