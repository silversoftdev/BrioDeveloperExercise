using DevTest.Services.UserManagement;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTest.ScheduleJobs
{
    [DisallowConcurrentExecution]
    public class ClaimsJobTrigger : IJob
    {
        private readonly ILogger<ClaimsJobTrigger> _logger;
        private readonly IUserManagement _userManagement;
        public ClaimsJobTrigger(ILogger<ClaimsJobTrigger> logger, IUserManagement userManagement)
        {
            _logger = logger;
            _userManagement = userManagement;
        }

        public Task Execute(IJobExecutionContext context)
        {

            var serviceCompleted = _userManagement.ClaimSchedule(1);
            _logger.LogInformation("Last RunTime" + context.ScheduledFireTimeUtc + " " + "Status : " + (serviceCompleted ? "Success" : "Failde"));
            return Task.CompletedTask;
        }
    }
}
