using CacheManager.Core;
using LightInject;
using LocationService.Api.Controllers;
using LocationService.Domain.Interfaces;
using LocationService.Domain.Services.Adresses;
using LocationService.Infrastructure.Services.Adresses;
using LocationService.Infrastructure.Services.Provider;
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

            var cache = CacheFactory.Build("getStartedCache", settings =>
            {
                settings.WithSystemRuntimeCacheHandle("handleName");
            });

            container.RegisterInstance(new AdressesService(new IAddressProvider[] { new ClientMailApi("http://www.buscacep.correios.com.br/", TimeSpan.FromSeconds(30)) }));

            container.RegisterInstance(new SearchAdressService(container.GetInstance<AdressesService>()));

            container.RegisterInstance(new AdressesController(container.GetInstance<SearchAdressService>()));

            container.ScopeManagerProvider = new PerLogicalCallContextScopeManagerProvider();

            return container;
        }

        private static IAddressProvider ClientMailApi()
        {
            throw new NotImplementedException();
        }
    }
}
