using AutoTestsForApplications.EmailSender;

namespace AutoTestsForApplications;

public class EmailSenderTests
{
    [Test]
    public async Task SendEmailTest()
    {
        var userNotifier = new UserNotifier();
        userNotifier.Notify(12);
        Console.WriteLine("test");
    }
}