using System;
using System.Net;
using System.Net.Mail;

class Program
{
    static void Main()
    {
        var message = new MailMessage();
        message.From = new MailAddress("youremail@example.com");
        message.To.Add("customer@example.com");
        message.Subject = "Reminder: Your Scheduled Task";
        message.Body = "Hello, this is your daily/weekly/monthly reminder.";

        var smtp = new SmtpClient("smtp.sendgrid.net")
        {
            Port = 587,
            Credentials = new NetworkCredential("apikey", "YOUR_SENDGRID_API_KEY"),
            EnableSsl = true
        };

        try
        {
            smtp.Send(message);
            Console.WriteLine("Email sent successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send email: {ex.Message}");
        }
    }
}
