using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace ServiceBusSendAndReceiveIsolated;

public class WriteMessages
{
    private readonly ILogger<WriteMessages> _logger;

    public WriteMessages(ILogger<WriteMessages> logger)
    {
        _logger = logger;
    }
    public class OutputType
    {
        [ServiceBusOutput("testlatencyqueue", Connection = "ServiceBusConnection")]
        public string[] OutputMessages { get; set; }

        public HttpResponseData HttpResponse { get; set; }
    }
    
    [Function("WriteMessages")]
    public  OutputType RunAsync(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]
        HttpRequestData req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        int count = int.Parse(req.Query["count"]!);
        var messages = Enumerable.Range(0, count).Select(x => $"Message Isolated {x}: {DateTimeOffset.Now:O}");

        return new OutputType()
        {
            HttpResponse = req.CreateResponse(HttpStatusCode.OK),
            OutputMessages = messages.ToArray()
        };
        
    }
}