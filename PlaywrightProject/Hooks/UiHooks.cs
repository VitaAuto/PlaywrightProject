using Microsoft.Playwright;
using PlaywrightProject.UI.Pages;
using Reqnroll;
using System.Threading.Tasks;
using System.Collections.Generic;
using PlaywrightProject.UI.Context;
using System.IO;

[Binding]
public class UiHooks
{
    private readonly TestContext _testContext;
    private readonly ScenarioContext _scenarioContext;
    private PlaywrightDriver? _driver;

    public UiHooks(ScenarioContext scenarioContext, TestContext testContext)
    {
        _scenarioContext = scenarioContext;
        _testContext = testContext;
    }

    [BeforeScenario]
    public async Task BeforeScenario()
    {
        _driver = new PlaywrightDriver();
        await _driver.InitAsync();
        _testContext.Page = _driver.Page;

        await _testContext.Page.SetExtraHTTPHeadersAsync(new Dictionary<string, string>
        {
            ["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36"
        });

        _testContext.CurrentPage = new MainPage(_testContext.Page);
    }

    [AfterStep]
    public async Task AfterStep()
    {
        if (_scenarioContext.TestError != null && _testContext.Page != null)
        {
            var fileName = $"screenshot_{System.DateTime.Now:yyyyMMdd_HHmmss_fff}.png";
            var filePath = Path.Combine("screenshots", fileName);
            Directory.CreateDirectory("screenshots");
            await _testContext.Page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = filePath,
                FullPage = true
            });
            System.Console.WriteLine($"Screenshot saved: {filePath}");
        }
    }

    [AfterScenario]
    public async Task AfterScenario()
    {
        if (_driver != null)
            await _driver.CleanupAsync();
    }
}