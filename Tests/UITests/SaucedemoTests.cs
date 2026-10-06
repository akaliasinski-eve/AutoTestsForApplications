using AutoTestsForApplications.DataProvider;
using AutoTestsForApplications.ForUI.Pages.SauceDemo;
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
        var isLabelVisible = await productsLabel.IsVisibleAsync();
        isLabelVisible.Should().BeTrue();
        CheckOutInformationPage checkoutInformationPage = new CheckOutInformationPage(Page);
        await checkoutInformationPage.FillCheckoutFormAsync("TestFirstName", "TestLastName", "1234567");
        await checkoutInformationPage.ClickContinueButtonAsync();
    }

    [Test]
    public async Task CheckThatOrderIsSuccessful()
    {
        LoginPageSD loginPageSd = new LoginPageSD(Page);
        await loginPageSd.OpenLoginPageAsync();
        await loginPageSd.LoginUserAsync("standard_user", "secret_sauce");
        ProductsPage productsPage = new ProductsPage(Page);
        await productsPage.CheckProductsPageIsOpenAsync();
        await productsPage.AddProductToCartByNameAsync("Sauce Labs Bolt T-Shirt");
        await productsPage.AddProductToCartByNameAsync("Sauce Labs Fleece Jacket");
        await productsPage.GoToCart();
        YourCartPage yourCartPage = new YourCartPage(Page);
        await yourCartPage.CheckItemIsInCartAsync("Sauce Labs Bolt T-Shirt");
        await yourCartPage.CheckItemIsInCartAsync("Sauce Labs Fleece Jacket");
        await yourCartPage.GoToCheckoutPageAsync();
        CheckOutInformationPage checkoutInformationPage = new CheckOutInformationPage(Page);
        await checkoutInformationPage.FillCheckoutFormAsync("TestFirstName", "TestLastName", "1234567");
        await checkoutInformationPage.ClickContinueButtonAsync();
        CheckoutOverviewPage checkoutOverviewPage = new CheckoutOverviewPage(Page);
        await checkoutOverviewPage.CheckItemIsInTheListAsync("Sauce Labs Bolt T-Shirt");
        await checkoutOverviewPage.CheckItemIsInTheListAsync("Sauce Labs Fleece Jacket");
        await checkoutOverviewPage.ClickFinishButtonAsync();
        CheckoutCompletePage checkoutCompletePage = new CheckoutCompletePage(Page);
        await checkoutCompletePage.CheckThankYouPhraseIsDisplayedAsync();
    }

    [TestCaseSource(typeof(TestCredentialsDataProvider),
        nameof(TestCredentialsDataProvider.GetCredentialsCases))] //параметризованный тест
    public async Task CredentialsValidatorTest(string login, string password)
    {
        LoginPageSD loginPageSd = new LoginPageSD(Page);
        await loginPageSd.OpenLoginPageAsync();
        await loginPageSd.LoginUserAsync(login, password);
        ProductsPage productsPage = new ProductsPage(Page);
        await productsPage.CheckProductsPageIsOpenAsync();
    }
}