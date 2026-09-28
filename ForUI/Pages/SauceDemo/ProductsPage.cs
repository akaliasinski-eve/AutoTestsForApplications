using FluentAssertions;
using Microsoft.Playwright;

namespace AutoTestsForApplications.ForUI.Pages.SauceDemo;

public class ProductsPage
{
    private readonly IPage Page;
    private ILocator ProductsLabel => Page.Locator("//span[text()='Products']");

    private ILocator AddToCartButton(string productName) =>
        Page.Locator(
            $"//*[text()='{productName}']/ancestor::div[@class='inventory_item']//button[text()='Add to cart']");

    private ILocator CartButton => Page.Locator("//a[@class='shopping_cart_link']");


    public ProductsPage(IPage page)
    {
        Page = page;
    }

    public async Task CheckProductsPageIsOpenAsync()
    {
        var isLabelVisible = await ProductsLabel.IsVisibleAsync();
        isLabelVisible.Should().BeTrue();
        await Assertions.Expect(Page).ToHaveURLAsync("https://www.saucedemo.com/inventory.html");
    }

    public async Task AddProductToCartByNameAsync(string productName)
    {
        await AddToCartButton(productName).ClickAsync();
    }

    public async Task GoToCart()
    {
        await CartButton.ClickAsync();
    }
}