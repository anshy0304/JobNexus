using JobNexus.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobNexus.Storage
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddStorage(this IServiceCollection services,string connectionString)
        {
            services.AddDbContext<JobDbContext>(options =>
                 options.UseSqlServer(connectionString));

            services.AddScoped<IJobRepository, JobRepository>();

            return services;
        }
    }
}
