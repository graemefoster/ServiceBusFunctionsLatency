using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace ServiceBusWriteAndRead;

public static class ReadMessages
{
    [FunctionName("ReadMessages")]
    public static void RunAsync([ServiceBusTrigger("testlatencyqueue", Connection = "ServiceBusConnection")] string myQueueItem, ILogger log)
    {
        log.LogInformation($"Processed: {myQueueItem} at {DateTimeOffset.Now:O}");
    }
}