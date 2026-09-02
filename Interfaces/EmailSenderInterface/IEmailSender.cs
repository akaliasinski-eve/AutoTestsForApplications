namespace AutoTestsForApplications.Interfaces.EmailSenderInterface;

public interface IEmailSender
{
    public void Send (string to, string text);
}