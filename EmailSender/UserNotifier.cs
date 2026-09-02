using AutoTestsForApplications.Interfaces.EmailSenderInterface;
using Microsoft.Extensions.DependencyInjection;

namespace AutoTestsForApplications.EmailSender;

public class UserNotifier
{
    public IServiceCollection serviceCollection = ServiceCollection.CreateServiceCollection();
    public ServiceProvider Provider => serviceCollection.BuildServiceProvider();
    
    public void Notify(int userId)
    {
        var emailSender = Provider.GetService<IEmailSender>();
        emailSender.Send("user@mail.com", $"Hello, user {userId}!");
    }
}