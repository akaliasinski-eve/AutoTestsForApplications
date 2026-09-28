using Microsoft.Playwright;

namespace AutoTestsForApplications.ForUI.Pages.Heroku;

public class AddRemovePage
{
    private readonly IPage Page;
    private ILocator Button(string buttonName) => Page.GetByRole(AriaRole.Button, new() { Name = $"{buttonName}" });


    public AddRemovePage(IPage page)
    {
        Page = page;
    }

    public async Task OpenAddRemovePageAsync()
    {
        await Page.GotoAsync("https://the-internet.herokuapp.com/add_remove_elements/");
    }

    public async Task CheckPageOpenAsync()
    {
        await Assertions.Expect(Page).ToHaveTitleAsync("The Internet");
        await Assertions.Expect(Page).ToHaveURLAsync("https://the-internet.herokuapp.com/add_remove_elements/");
    }

    public async Task ClickButtonByNameAsync(string buttonName)
    {
        await Button(buttonName).ClickAsync();
    }

    public async Task CheckNumberofButtonsAsync(string buttonName, int count)
    {
        await Assertions.Expect(Button(buttonName)).ToHaveCountAsync(count);
    }

    public async Task ClickButtonByNameAndIndexAsync(string buttonName, int number)
    {
        await Button(buttonName).Nth(number - 1).ClickAsync();
    }
}