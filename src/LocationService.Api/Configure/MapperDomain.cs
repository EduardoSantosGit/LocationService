using AutoMapper;
using LocationService.Domain.Models;
using LocationService.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationService.Api.Configure
{
    public class MapperDomain
    {
        private static readonly object _locker = new object();
        private static bool _intialized = false;

        public static void Configure()
        {
            lock (_locker)
            {
                // Check if it has already run
                if (_intialized)
                { return; }

                ConfigureInternal();

                // Mark as run
                _intialized = true;
            }
        }

        private static void ConfigureInternal()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Address, AddressResource>();
            });
        }
    }
}
