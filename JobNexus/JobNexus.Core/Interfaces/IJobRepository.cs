using JobNexus.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobNexus.Core.Interfaces
{
    public interface IJobRepository
    {
        
        Task<BackgroundJob?> GetNextPendingJobAsync();

        
        Task UpdateJobAsync(BackgroundJob job);
    }
}
