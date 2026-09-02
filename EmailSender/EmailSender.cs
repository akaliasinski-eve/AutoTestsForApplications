using AutoTestsForApplications.Interfaces.EmailSenderInterface;

namespace AutoTestsForApplications.EmailSender;

public class EmailSender : IEmailSender
{
    public void Send(string to, string text)
    {
        Console.WriteLine($"Sending mail to {to}: {text}");
    }
}