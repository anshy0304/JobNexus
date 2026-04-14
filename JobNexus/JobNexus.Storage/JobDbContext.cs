using JobNexus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobNexus.Storage
{
    public class JobDbContext : DbContext
    {
        public JobDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<BackgroundJob> Jobs { get; set; }

    }
}
