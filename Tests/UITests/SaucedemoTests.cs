using FluentAssertions;
using Microsoft.Playwright;

namespace AutoTestsForApplications.UITests;

public class SaucedemoTests : BaseTest
{
    [Test]
    public async Task CheckSuccessfulLogin()
    {
        await Page.GotoAsync("https://www.saucedemo.com/");
        var usernameField = Page.GetByRole(AriaRole.Textbox, new() { Name = "Username" });
        var passwordField = Page.GetByRole(AriaRole.Textbox, new() { Name = "Password" });
        await usernameField.FillAsync("standard_user");
        await passwordField.FillAsync("secret_sauce");
        var loginButton = Page.GetByRole(AriaRole.Button, new() { Name = "Login" });
        await loginButton.ClickAsync();
        var productsLabel = Page.Locator("//span[text()='Products']");
        await productsLabel.IsVisibleAsync();
    }
}