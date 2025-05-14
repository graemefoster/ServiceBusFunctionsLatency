using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ServiceBusWriteAndRead;

public static class WriteMessages
{
    [FunctionName("WriteMessages")]
    public static async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]
        HttpRequest req,
        [ServiceBus("testlatencyqueue", Connection = "ServiceBusConnection")] IAsyncCollector<string> message,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        int count = int.Parse(req.Query["count"]);
        for(int i = 0; i < count; i++)
        {
            await message.AddAsync($"Message {i}: {DateTimeOffset.Now:O}");
        }

        return new OkObjectResult(count);
        
    }
}