using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CoBySi.Function
{
    public class TimerTriggerPing
    {
        private readonly ILogger _logger;

        public TimerTriggerPing(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<TimerTriggerPing>();
        }

        [Function("TimerTriggerPing")]
        public async Task Run([TimerTrigger("0 */3 * * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://www.codebysimon.com");
                _logger.LogError("HTTP request status code: {StatusCode}", response.StatusCode);
            }

            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }
        }
    }
}
