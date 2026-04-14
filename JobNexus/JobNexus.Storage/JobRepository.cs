using JobNexus.Core.Entities;
using JobNexus.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobNexus.Storage
{
    public class JobRepository : IJobRepository
    {
        private readonly JobDbContext _context;
        public JobRepository(JobDbContext context)
        {
            _context = context;
        }

        public async Task<BackgroundJob?> GetNextPendingJobAsync()
        {
            return await _context.Jobs
                .FirstOrDefaultAsync(j => j.Status == Core.Enums.JobStatus.Pending);
        }
        public async Task UpdateJobAsync(BackgroundJob job)
        {
            _context.Jobs.Update(job);
            await _context.SaveChangesAsync();
        }
    }
}
