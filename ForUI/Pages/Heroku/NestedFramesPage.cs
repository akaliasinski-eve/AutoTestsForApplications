using Microsoft.Playwright;

namespace AutoTestsForApplications.ForUI.Pages.Heroku;

public class NestedFramesPage
{
    private readonly IPage Page;

    private ILocator LeftFrame => Page.FrameLocator("frame[name='frame-top']")
        .FrameLocator("frame[name='frame-left']").Locator("body");
    
    public  NestedFramesPage(IPage page)
    {
        Page = page;
    }

    public async Task<string> GetTextFromLeftFrameAsync()
    {
        return await LeftFrame.InnerTextAsync();
    }
}