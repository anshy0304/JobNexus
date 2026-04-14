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
                using(var scope = _scopeFactory.CreateScope())
                {
                    var jobRepository = scope.ServiceProvider.GetRequiredService<IJobRepository>();

                    var currentJob = await jobRepository.GetNextPendingJobAsync();

                    if(currentJob == null)
                    {
                        _logger.LogInformation("No jobs found.Going back to sleep....at{time}", DateTimeOffset.Now);
                    }else
                    {
                        _logger.LogInformation("Found job with ID:{jobId}! Processing...", currentJob.Id);

                        await Task.Delay(2000, stoppingToken);

                        currentJob.Status = JobStatus.Completed;

                        await jobRepository.UpdateJobAsync(currentJob);

                        _logger.LogInformation("Job {JobId} successfully completed and saved!", currentJob.Id);
                    }
                }
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
