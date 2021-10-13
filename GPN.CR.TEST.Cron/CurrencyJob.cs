using System;
using System.Threading.Tasks;
using GPN.CR.TEST.Cron.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;

namespace GPN.CR.TEST.Cron
{
    [DisallowConcurrentExecution]
    public class CurrencyJob : IJob
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<CurrencyJob> _logger;
        public CurrencyJob(IServiceScopeFactory serviceScopeFactory, ILogger<CurrencyJob> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var loadCurSrv = scope.ServiceProvider.GetService<ILoadCurrencyService>();
                    await loadCurSrv.SaveToDb();
                }

                _logger.LogInformation($"{DateTime.Now:g}: валюта успешно загружена");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now:g}: ошибка загрузки валюты");
            }
        }
    }
}
