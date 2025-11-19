using Microsoft.Playwright;
using PlaywrightProject.Context;
using PlaywrightProject.Pages;
using Reqnroll;
using System.Threading.Tasks;


[Binding]
public class Hooks(TestContext testContext)
{
    private readonly TestContext _testContext = testContext;
    private PlaywrightDriver? _driver;

    [BeforeScenario]
    public async Task BeforeScenario()
    {
        _driver = new PlaywrightDriver();
        await _driver.InitAsync();
        _testContext.Page = _driver.Page;
        // По умолчанию открываем MainPage
        _testContext.CurrentPage = new MainPage(_testContext.Page);
    }

    [AfterScenario]
    public async Task AfterScenario()
    {
        if (_driver != null)
            await _driver.CleanupAsync();
    }
}