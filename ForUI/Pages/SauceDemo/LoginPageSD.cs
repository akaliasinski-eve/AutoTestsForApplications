using Microsoft.Playwright;

namespace AutoTestsForApplications.ForUI.Pages.SauceDemo;

public class LoginPageSD
{
    private readonly IPage Page;
    private ILocator UsernameField => Page.GetByRole(AriaRole.Textbox, new() { Name = "Username" });
    private ILocator PasswordField => Page.GetByRole(AriaRole.Textbox, new() { Name = "Password" });
    private ILocator LoginButton => Page.GetByRole(AriaRole.Button, new() { Name = "Login" });

    public LoginPageSD(IPage page)
    {
        Page = page;
    }

    public async Task OpenLoginPageAsync()
    {
        await Page.GotoAsync("https://www.saucedemo.com/");
    }

    public async Task LoginUserAsync(string username, string password)
    {
        await UsernameField.FillAsync(username);
        await PasswordField.FillAsync(password);
        await LoginButton.ClickAsync();
    }
}