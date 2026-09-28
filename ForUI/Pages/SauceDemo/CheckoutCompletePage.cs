using Microsoft.Playwright;

namespace AutoTestsForApplications.ForUI.Pages.SauceDemo;

public class CheckoutCompletePage
{
    private readonly IPage Page;
    private ILocator ThankYouPhrase => Page.Locator("//h2[text()='Thank you for your order!']");

    public CheckoutCompletePage(IPage page)
    {
        Page = page;
    }

    public async Task CheckThankYouPhraseIsDisplayedAsync()
    {
        await Assertions.Expect(ThankYouPhrase).ToBeVisibleAsync();
    }
}