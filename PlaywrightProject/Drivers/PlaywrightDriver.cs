using Microsoft.Playwright;

public class PlaywrightDriver
{
    public IPlaywright? Playwright { get; private set; }
    public IBrowser? Browser { get; private set; }
    public IBrowserContext? Context { get; private set; }
    public IPage? Page { get; private set; }

    public async Task InitAsync(int viewportWidth = 1920, int viewportHeight = 1080)
    {
        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true,
            Args = new[] { $"--window-size={viewportWidth},{viewportHeight}" }
        });

        Context = await Browser.NewContextAsync(new BrowserNewContextOptions
        {
            ViewportSize = new ViewportSize
            {
                Width = viewportWidth,
                Height = viewportHeight
            }
        });

        Page = await Context.NewPageAsync();
    }

    public async Task CleanupAsync()
    {
        if (Browser != null)
            await Browser.CloseAsync();
        Playwright?.Dispose();
    }
}
