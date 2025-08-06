using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

class Program
{
    static async Task Main()
    {
        string accessToken = "<YOUR_AZURE_BEARER_TOKEN>";
        string subscriptionId = "<YOUR_SUBSCRIPTION_ID>";
        string resourceGroupName = "<RESOURCE_GROUP>";
        string appName = "<APP_SERVICE_NAME>";

        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var requestUrl = $"https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Web/sites/{appName}/config/web?api-version=2022-09-01";

        var payload = new JObject
        {
            ["properties"] = new JObject
            {
                ["httpsOnly"] = true
            }
        };

        var response = await httpClient.PutAsync(requestUrl, new StringContent(payload.ToString(), System.Text.Encoding.UTF8, "application/json"));

        if (response.IsSuccessStatusCode)
            Console.WriteLine("HTTPS enforced successfully.");
        else
            Console.WriteLine("Failed: " + await response.Content.ReadAsStringAsync());
    }
}
