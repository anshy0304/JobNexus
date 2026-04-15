using JobNexus.Core.Entities;
using JobNexus.Core.Enums;
using JobNexus.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobNexus.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IJobRepository _repository;
        public JobsController(IJobRepository repository)
        {
            _repository = repository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateJob()
        {
            var newJob = new BackgroundJob
            {
                Name = "API Test Job",
                Status = JobStatus.Pending
            };
            await _repository.AddAsync(newJob);
            return Ok("Job created successfully");
        }
    }
}
