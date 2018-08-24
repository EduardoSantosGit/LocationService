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



    }
}
