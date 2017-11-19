using LightInject;
using LocationService.Api.Controllers;
using LocationService.Domain.Services.Adresses;
using LocationService.Infrastructure.Services.Adresses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace LocationService.Api.Configure
{
    public class InjectionDependency
    {
        public static ServiceContainer ConfigureService()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");

            var containerOptions = new ContainerOptions { EnablePropertyInjection = false };
            var container = new ServiceContainer(containerOptions);

            container.RegisterInstance(new AdressesService(new AdressesClientApi()));

            container.RegisterInstance(new SearchAdressService(container.GetInstance<AdressesService>()));

            container.RegisterInstance(new AdressesController(container.GetInstance<SearchAdressService>()));

            container.ScopeManagerProvider = new PerLogicalCallContextScopeManagerProvider();

            return container;
        }
    }
}
