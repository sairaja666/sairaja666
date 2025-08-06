using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        string accessToken = "<YOUR_AZURE_BEARER_TOKEN>";
        string subscriptionId = "<SUBSCRIPTION_ID>";
        string appServiceName = "<APP_SERVICE_NAME>";
        string resourceGroup = "<RESOURCE_GROUP>";
        string certThumbprint = "<NEW_CERT_THUMBPRINT>";

        string apiUrl = $"https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroup}/providers/Microsoft.Web/sites/{appServiceName}/hostNameBindings/<hostname>?api-version=2022-09-01";

        var payload = $@"{{
            ""properties"": {{
                ""sslState"": ""SniEnabled"",
                ""thumbprint"": ""{certThumbprint}"",
                ""toUpdate"": true
            }}
        }}";

        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

        var content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");
        var result = await httpClient.PutAsync(apiUrl, content);

        Console.WriteLine(await result.Content.ReadAsStringAsync());
    }
}
