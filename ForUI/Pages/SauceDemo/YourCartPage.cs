using Microsoft.Playwright;

namespace AutoTestsForApplications.ForUI.Pages.SauceDemo;

public class YourCartPage
{
    private readonly IPage Page;
    private ILocator ItemInCart(string itemName) => Page.Locator($"//div[text()='{itemName}']");
    private ILocator CheckoutButton => Page.GetByRole(AriaRole.Button, new() { Name = "Checkout" });

    public YourCartPage(IPage page)
    {
        Page = page;
    }

    public async Task CheckItemIsInCartAsync(string itemName)
    {
        await Assertions.Expect(ItemInCart(itemName)).ToBeVisibleAsync();
    }

    public async Task GoToCheckoutPageAsync()
    {
        await CheckoutButton.ClickAsync();
    }
}