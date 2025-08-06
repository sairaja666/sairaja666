using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

class Program
{
    // Replace these with your values
    static string accessToken = "<YOUR_AZURE_BEARER_TOKEN>";
    static string subscriptionId = "<YOUR_SUBSCRIPTION_ID>";
    // If you want to scan all resource groups, you can fetch them dynamically too

    static async Task Main()
    {
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        Console.WriteLine("Fetching list of App Services...");

        // List all web apps in subscription (API version might need updating)
        var listAppsUrl = $"https://management.azure.com/subscriptions/{subscriptionId}/providers/Microsoft.Web/sites?api-version=2022-09-01";

        var response = await httpClient.GetAsync(listAppsUrl);
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Failed to list App Services: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
            return;
        }

        var content = await response.Content.ReadAsStringAsync();
        var json = JObject.Parse(content);

        Console.WriteLine("App Services found:");

        foreach (var app in json["value"])
        {
            string name = app["name"].ToString();
            string resourceGroup = ExtractResourceGroupFromId(app["id"].ToString());

            // Get config for each app
            string configUrl = $"https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroup}/providers/Microsoft.Web/sites/{name}/config/web?api-version=2022-09-01";

            var configResponse = await httpClient.GetAsync(configUrl);
            if (!configResponse.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to get config for {name}: {configResponse.StatusCode}");
                continue;
            }

            var configContent = await configResponse.Content.ReadAsStringAsync();
            var configJson = JObject.Parse(configContent);

            bool httpsOnly = configJson["properties"]?["httpsOnly"]?.ToObject<bool>() ?? false;

            Console.WriteLine($"- {name} (RG: {resourceGroup}) HTTPS Only: {(httpsOnly ? "Enabled" : "Disabled")}");
        }
    }

    static string ExtractResourceGroupFromId(string resourceId)
    {
        // resourceId example: /subscriptions/{subId}/resourceGroups/{rg}/providers/Microsoft.Web/sites/{appName}
        var parts = resourceId.Split('/');
        for (int i = 0; i < parts.Length; i++)
        {
            if (parts[i].Equals("resourceGroups", StringComparison.OrdinalIgnoreCase) && i + 1 < parts.Length)
            {
                return parts[i + 1];
            }
        }
        return null;
    }
}
