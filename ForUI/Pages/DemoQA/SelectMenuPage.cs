using Microsoft.Playwright;

namespace AutoTestsForApplications.ForUI.Pages.DemoQA;

public class SelectMenuPage
{
    private readonly IPage Page;
    private ILocator DropdownSelectOne => Page.Locator("//*[@id='react-select-3-input']");
    private ILocator ElementInDropdown(string elementName) => Page.Locator($"//*[text()='{elementName}']");

    public SelectMenuPage(IPage page)
    {
        Page = page;
    }

    public async Task OpenSelectMenuPageAsync()
    {
        await Page.GotoAsync("https://demoqa.com/select-menu");
    }

    public async Task SelectItemInDropdownAsync(string itemName)
    {
        await DropdownSelectOne.ClickAsync();
        await ElementInDropdown(itemName).ClickAsync();
    }

    public async Task CheckSelectedItemIsDisplayedAsync(string itemName)
    {
        await Assertions.Expect(ElementInDropdown(itemName)).ToBeVisibleAsync();
    }
}