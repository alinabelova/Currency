using System;
using GPN.CR.TEST.Bussines.Models;
using GPN.CR.TEST.Bussines.Modules;
using GPN.CR.TEST.Cron;
using GPN.CR.TEST.Cron.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace GPN.CR.TEST.Host.Modules
{
    public static class WebModule
    {
        public static IServiceCollection AddWebModule(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            services.AddSingleton<IMemoryCache, MemoryCache>();

            services.AddSingleton(c => c.GetService<IOptions<MainConfig>>().Value);

            services.AddServiceModule();
            services.AddTransient<ILoadCurrencyService, LoadXmlCurrencyService>();
            return services;
        }
    }
}
