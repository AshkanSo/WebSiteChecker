using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SystemLogger
{
    public class ScopedWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ScopedWorker> _logger;

        public ScopedWorker(IServiceProvider serviceProvider, ILogger<ScopedWorker> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var worker = scope.ServiceProvider.GetRequiredService<Worker>();
                    await worker.ExecuteTask(stoppingToken);
                }

                await Task.Delay(15000, stoppingToken);
            }
        }
    }
}