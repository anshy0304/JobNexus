using JobNexus.Core.Interfaces;
using JobNexus.Storage;

namespace JobNexus.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("JobNexus Worker is starting up.. ");
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker checking for jobs at : {time}", DateTimeOffset.Now);

                using (var scope = _scopeFactory.CreateScope())
                {
                    var jobRepository = scope.ServiceProvider.GetRequiredService<IJobRepository>();
                }
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
