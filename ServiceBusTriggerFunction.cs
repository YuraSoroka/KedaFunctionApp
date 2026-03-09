using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace KedaFunctionApp;

public class ServiceBusTriggerFunction
{
    private readonly ILogger<ServiceBusTriggerFunction> _logger;

    public ServiceBusTriggerFunction(ILogger<ServiceBusTriggerFunction> logger)
    {
        _logger = logger;
    }

    [Function(nameof(ServiceBusTriggerFunction))]
    public async Task Run(
        [ServiceBusTrigger("keda", "func-subscription", Connection = "ServiceBusConnection")]
        ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions)
    {
        _logger.LogInformation("Message ID: {id}", message.MessageId);
        _logger.LogInformation("Message Body: {body}", message.Body);
        _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

        await Task.Delay(5000); // work simulation

        // Complete the message
        await messageActions.CompleteMessageAsync(message);
    }
}