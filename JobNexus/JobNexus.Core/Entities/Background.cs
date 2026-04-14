using JobNexus.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobNexus.Core.Entities
{
    public class BackgroundJob
    {
        public Guid Id { get; set; } = Guid.NewGuid(); 
        public string Name { get; set; } = string.Empty; 
        public string Payload { get; set; } = string.Empty;
        public JobStatus Status { get; set; } = JobStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
