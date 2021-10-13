using GPN.CR.TEST.Cron;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Quartz;

namespace GPN.CR.TEST.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddQuartz(q =>
                    {
                        q.UseMicrosoftDependencyInjectionScopedJobFactory();
                        var jobKey = new JobKey("LoadCurrencyFromCbrJob");
                        q.AddJob<CurrencyJob>(opts => opts.WithIdentity(jobKey));
                        q.AddTrigger(opts => opts
                            .ForJob(jobKey) 
                            .WithIdentity("LoadCurrencyFromCbrJob-trigger")
                            .WithCronSchedule("0 0 12 * * ?"));
                    });
                    services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
                }).ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
