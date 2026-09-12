using FluentAssertions;
using Microsoft.Playwright;

namespace AutoTestsForApplications.UITests;

public class HerokuTests : BaseTest
{
    [Test]
    public async Task CheckBoxTest()
    {
        await Page.GotoAsync("https://the-internet.herokuapp.com/checkboxes");
        var firstCheckbox = Page.Locator("input[type='checkbox']").Nth(0);
        await firstCheckbox.CheckAsync();
        (await firstCheckbox.IsCheckedAsync()).Should().BeTrue();
    }

    [Test]
    public async Task FormAuthentication()
    {
        await Page.GotoAsync("https://the-internet.herokuapp.com/login");
        var loginTextBox = Page.GetByRole(AriaRole.Textbox, new() { Name = "Username"});
        var passTextBox = Page.GetByRole(AriaRole.Textbox, new() { Name = "Password"});
        await loginTextBox.FillAsync("wrong");
        await passTextBox.FillAsync("wrong");
        var loginButton = Page.GetByRole(AriaRole.Button, new() { Name = "Login"});
        await loginButton.ClickAsync();
        var errorMessage = Page.Locator("//div[contains(text(),'Your username is invalid!')]");
        var state = await errorMessage.IsVisibleAsync();
        state.Should().BeTrue();
        var errorMessage2 = await Page.QuerySelectorAsync("#flash");
        var textTextErrorMessage = await errorMessage2.InnerTextAsync();
        textTextErrorMessage.Should().Contain("Your username is invalid!");
    }
    
}