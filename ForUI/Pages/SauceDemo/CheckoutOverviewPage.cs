using Microsoft.Playwright;

namespace AutoTestsForApplications.ForUI.Pages.SauceDemo;

public class CheckoutOverviewPage
{
    private readonly IPage Page;
    private ILocator ItemInCheckoutOverview(string itemName) => Page.Locator($"//div[text()='{itemName}']");
    private ILocator FinishButton => Page.GetByRole(AriaRole.Button, new() { Name = "Finish" });

    public CheckoutOverviewPage(IPage page)
    {
        Page = page;
    }

    public async Task CheckItemIsInTheListAsync(string itemName)
    {
        await Assertions.Expect(ItemInCheckoutOverview(itemName)).ToBeVisibleAsync();
    }

    public async Task ClickFinishButtonAsync()
    {
        await FinishButton.ClickAsync();
    }
}