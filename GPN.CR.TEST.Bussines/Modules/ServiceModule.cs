using System;
using System.IO;
using GPN.CR.TEST.Bussines.Interfaces;
using GPN.CR.TEST.Bussines.Services;
using GPN.CR.TEST.Data.EF;
using GPN.CR.TEST.Data.Interfaces;
using GPN.CR.TEST.Data.Models;
using GPN.CR.TEST.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GPN.CR.TEST.Bussines.Modules
{
    public static class ServiceModule
    {
        public static IServiceCollection AddServiceModule(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            IConfigurationRoot config = builder.Build();

            services.AddDbContext<ApplicationContext>(x =>
                x.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            services.AddTransient<IRepository<Currency>, CurrencyRepository>();
            services.AddTransient<IRepository<CurrencyDaily>, CurrencyDailyRepository>();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddTransient<ICurrencyDailyService, CurrencyDailyService>();

            return services;
        }
    }
}
