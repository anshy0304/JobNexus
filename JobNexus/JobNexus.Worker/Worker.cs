using JobNexus.Core.Entities;
using JobNexus.Core.Enums;
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
                BackgroundJob? currentJob = null;

                using var scope = _scopeFactory.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<IJobRepository>();

                try
                {
                    currentJob = await repository.GetNextPendingJobAsync();

                    if (currentJob != null)
                    {
                        
                        currentJob.Status = JobStatus.Processing;
                        await repository.UpdateJobAsync(currentJob);

                        await Task.Delay(2000, stoppingToken);

                        currentJob.Status = JobStatus.Completed;
                        await repository.UpdateJobAsync(currentJob);
                    }
                }
                catch (Exception)
                {
                    if (currentJob != null)
                    {
                        currentJob.Status = JobStatus.Failed;
                        await repository.UpdateJobAsync(currentJob);
                    }
                }

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
