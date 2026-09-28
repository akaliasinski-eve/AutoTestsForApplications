using AutoTestsForApplications.ForUI.Pages.Heroku;
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
        LoginPage loginPage = new LoginPage(Page);
        await loginPage.OpenLoginPageAsync();
        await loginPage.LoginUser("wrongUsername", "wrongPassword");
        var errorMessage = await loginPage.GetTextFromErrorMessageLabel();
        errorMessage.Should().Contain("Your username is invalid!");
    }

    [Test]
    public async Task DropDown()
    {
        await Page.GotoAsync("https://the-internet.herokuapp.com/dropdown");
        await Assertions.Expect(Page).ToHaveTitleAsync("The Internet");
        await Assertions.Expect(Page).ToHaveURLAsync("https://the-internet.herokuapp.com/dropdown");

        //стандартный дропдаун
        var dropdown = Page.Locator("#dropdown");
        await Assertions.Expect(dropdown).ToBeVisibleAsync();
        await dropdown.SelectOptionAsync("1");
        await Assertions.Expect(dropdown).ToHaveValueAsync("1");
        var selected1 = dropdown.Locator("option:checked");
        await Assertions.Expect(selected1).ToHaveTextAsync("Option 1");
        var text = await dropdown.InnerTextAsync();
        text.Should().Contain("Option 1");

        await dropdown.SelectOptionAsync("2");
        await Assertions.Expect(dropdown).ToHaveValueAsync("2");
        var selected2 = dropdown.Locator("option:checked");
        await Assertions.Expect(selected1).ToHaveTextAsync("Option 2");
        var text2 = await dropdown.InnerTextAsync();
        text2.Should().Contain("Option 2");

        var opt1 = Page.Locator("//option[@selected='selected']");
        await Assertions.Expect(opt1).ToHaveTextAsync("Option 2");

        //нестандартный дропдаун
        await dropdown.ClickAsync();
        var option2 = Page.Locator("//option[text()='Option 2']");
        await option2.ClickAsync();
        var textFromDropdown = await option2.InnerTextAsync();
        textFromDropdown.Should().Be("Option 2");
    }

    [Test]
    public async Task AddRemoveElements()
    {
        AddRemovePage addRemovePage = new AddRemovePage(Page);
        await addRemovePage.OpenAddRemovePageAsync();
        await addRemovePage.CheckPageOpenAsync();

        await addRemovePage.ClickButtonByNameAsync("Add element");
        await addRemovePage.CheckNumberofButtonsAsync("Delete", 1);
        await addRemovePage.ClickButtonByNameAsync("Add element");
        await addRemovePage.CheckNumberofButtonsAsync("Delete", 2);

        await addRemovePage.ClickButtonByNameAndIndexAsync("Delete", 2);
        await addRemovePage.CheckNumberofButtonsAsync("Delete", 1);
    }

    [Test]
    public async Task LeftFrameTest()
    {
        FramesPage framesPage = new FramesPage(Page);
        await framesPage.OpenFramesPageAsync();
        await framesPage.ClickNestedFramesLinkAsync();
        NestedFramesPage nestedFramesPage = new NestedFramesPage(Page);
        var leftText = await nestedFramesPage.GetTextFromLeftFrameAsync();
        leftText.Should().Contain("LEFT");
    }
}