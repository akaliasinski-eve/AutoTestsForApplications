using Microsoft.Playwright;

namespace AutoTestsForApplications.ForUI.Pages.Heroku;

public class WindowsPage
{
    private readonly IPage Page;
    private ILocator ClickHereLink => Page.GetByRole(AriaRole.Link, new(){Name = "Click Here"});
    
    public WindowsPage(IPage page)
    {
        Page = page;
    }

    public async Task OpenMultipleWindowsPageAsync()
    {
        await Page.GotoAsync("https://the-internet.herokuapp.com/windows");
    }

    public async Task ClickClickHereLinkAsync()
    {
        await ClickHereLink.ClickAsync();
    }

    public async Task<IPage> OpenNewWindowAsync()
    {
        return await Page.RunAndWaitForPopupAsync(async () =>
        {
            await ClickHereLink.ClickAsync();
        });
    }
}