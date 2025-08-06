using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<string> accounts = new List<string> { "user1@domain.com", "user2@domain.com" };
        List<string> deprecatedAccounts = new List<string>();

        foreach (var user in accounts)
        {
            bool isDeprecated = CheckIfUserIsDeprecated(user); // Placeholder
            if (isDeprecated)
            {
                Console.WriteLine($"[!] Marked for removal: {user}");
                deprecatedAccounts.Add(user);
            }
        }

        // Perform deletion or RBAC role removal logic here
    }

    static bool CheckIfUserIsDeprecated(string user)
    {
        // Simulate check (replace with actual Graph API / Azure CLI logic)
        return user.StartsWith("user2");
    }
}
