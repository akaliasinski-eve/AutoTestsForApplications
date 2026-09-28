using Microsoft.Playwright;

namespace AutoTestsForApplications.ForUI.Pages.SauceDemo;

public class CheckOutInformationPage
{
    private readonly IPage Page;
    private ILocator FirstNameField => Page.Locator("//input[@id='first-name']");
    private ILocator LastNameField => Page.Locator("//input[@id='last-name']");
    private ILocator ZipCodeField => Page.Locator("//input[@id='postal-code']");
    private ILocator ContinueButton => Page.GetByRole(AriaRole.Button, new() { Name = "Continue" });

    public CheckOutInformationPage(IPage page)
    {
        Page = page;
    }

    public async Task FillCheckoutFormAsync(string firstName, string lastName, string zipCode)
    {
        await FirstNameField.FillAsync(firstName);
        await LastNameField.FillAsync(lastName);
        await ZipCodeField.FillAsync(zipCode);
    }

    public async Task ClickContinueButtonAsync()
    {
        await ContinueButton.ClickAsync();
    }
}