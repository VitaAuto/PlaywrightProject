using Microsoft.Playwright;
using ApiAndUiProject.API.ApiClient;
using ApiAndUiProject.API.Auth;
using ApiAndUiProject.API.Context;
using ApiAndUiProject.API.Services;
using ApiAndUiProject.Config;
using ApiAndUiProject.UI;
using ApiAndUiProject.UI.Helpers;
using Reqnroll;
using Reqnroll.BoDi;

namespace ApiAndUiProject.Hooks
{
    [Binding]
    public class DependencyInjection
    {
        [BeforeTestRun]
        public static async Task RegisterDependencies(ObjectContainer container)
        {
            var settings = ConfigReader.LoadSettings();

            var playwright = await Playwright.CreateAsync();

            IBrowser browser = settings?.Browser?.ToLower() switch
            {
                "chrome" => await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = settings.Headless }),
                "firefox" => await playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions { Headless = settings.Headless }),
                _ => throw new ArgumentException($"Unknown browser type: {settings?.Browser}")
            };

            container.RegisterInstanceAs(playwright);
            container.RegisterInstanceAs(browser);

            container.RegisterTypeAs<ElementFinder, IElementFinder>();

            container.RegisterTypeAs<TokenProvider, ITokenProvider>();
            container.RegisterFactoryAs<UsersApiClient>(c => new UsersApiClient(ApiConfig.ApiBaseUrl, c.Resolve<ITokenProvider>()));
            container.RegisterTypeAs<UserService, UserService>();
        }
    }
}