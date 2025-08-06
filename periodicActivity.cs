using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

public static class PeriodicScannerFunction
{
    [FunctionName("PeriodicScanner")]
    public static async Task Run(
        [TimerTrigger("0 0 8 * * *")] TimerInfo myTimer, ILogger log)
    {
        log.LogInformation($"Periodic scanning started at: {DateTime.Now}");

        // Call your HTTPS scan or email sending code here
        await YourScanOrEmailMethod();

        log.LogInformation($"Periodic scanning finished at: {DateTime.Now}");
    }

    private static Task YourScanOrEmailMethod()
    {
        // Your logic here
        return Task.CompletedTask;
    }
}
