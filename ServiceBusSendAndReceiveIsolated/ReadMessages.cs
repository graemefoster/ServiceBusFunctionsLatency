using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ServiceBusSendAndReceiveIsolated;

public class ReadMessages
{
    private readonly ILogger<ReadMessages> _logger;

    public ReadMessages(ILogger<ReadMessages> logger)
    {
        _logger = logger;
    }

    [Function("ReadMessages")]
    public void RunAsync(
        [ServiceBusTrigger("testlatencyqueue", Connection = "ServiceBusConnection")] string myQueueItem)
    {
        _logger.LogInformation($"Isolated Processed: {myQueueItem} at {DateTimeOffset.Now:O}");
    }
}