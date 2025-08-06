using System;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

class Program
{
    static void Main()
    {
        var kvUri = "https://<your-keyvault-name>.vault.azure.net/";
        var secretName = "<your-secret-name>";
        var newSecretValue = Guid.NewGuid().ToString(); // or retrieve from a source

        var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());

        client.SetSecret(secretName, newSecretValue);
        Console.WriteLine($"Secret '{secretName}' rotated successfully at {DateTime.UtcNow}");
    }
}
