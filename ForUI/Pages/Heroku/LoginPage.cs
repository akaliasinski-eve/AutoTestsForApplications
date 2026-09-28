using Microsoft.Playwright;

namespace AutoTestsForApplications.ForUI.Pages.Heroku;

public class LoginPage
{
    private readonly IPage Page;
    private ILocator LoginTextBox => Page.GetByRole(AriaRole.Textbox, new() { Name = "Username" });
    private ILocator PassTextBox => Page.GetByRole(AriaRole.Textbox, new() { Name = "Password" });
    private ILocator PloginButton => Page.GetByRole(AriaRole.Button, new() { Name = "Login" });
    private ILocator ErrorMessage => Page.Locator("//div[@id='flash']");

    public LoginPage(IPage page)
    {
        Page = page;
    }

    public async Task OpenLoginPageAsync()
    {
        await Page.GotoAsync("https://the-internet.herokuapp.com/login");
    }

    public async Task LoginUser(string username, string password)
    {
        await LoginTextBox.FillAsync(username);
        await PassTextBox.FillAsync(password);
        await PloginButton.ClickAsync();
    }

    public async Task<string> GetTextFromErrorMessageLabel()
    {
        return await ErrorMessage.TextContentAsync();
    }
}