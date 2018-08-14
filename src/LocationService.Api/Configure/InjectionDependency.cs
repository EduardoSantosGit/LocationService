using CacheManager.Core;
using LightInject;
using LocationService.Api.Controllers;
using LocationService.Domain.Interfaces;
using LocationService.Domain.Models;
using LocationService.Infrastructure.Services.Provider;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using LocationService.Infrastructure.Services.Addresses;
using LocationService.Domain.Services.Addresses;

namespace LocationService.Api.Configure
{
    public class InjectionDependency
    {
        public static ServiceContainer ConfigureService()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");

            var containerOptions = new ContainerOptions { EnablePropertyInjection = false };
            var container = new ServiceContainer(containerOptions);

            container.RegisterInstance(new AddressesService(new IAddressProvider[] { new ClientMailApi("http://www.buscacep.correios.com.br/", TimeSpan.FromSeconds(30)) }));

            container.RegisterInstance(new SearchAddressService(container.GetInstance<AddressesService>()));

            container.RegisterInstance(new AddressesController(container.GetInstance<SearchAddressService>()));

            container.ScopeManagerProvider = new PerLogicalCallContextScopeManagerProvider();

            return container;
        }
    }
    
}
