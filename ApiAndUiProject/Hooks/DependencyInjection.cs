using Amazon.SQS;
using ApiAndUiProject.API.Auth;
using ApiAndUiProject.API.Clients;
using ApiAndUiProject.API.Context;
using ApiAndUiProject.API.Services;
using ApiAndUiProject.Config;
using ApiAndUiProject.Steps;
using ApiAndUiProject.UI;
using ApiAndUiProject.UI.Helpers;
using Microsoft.Extensions.Logging;
using Microsoft.Playwright;
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

            var sqsClient = new AmazonSQSClient(new AmazonSQSConfig
            {
                ServiceURL = ApiConfig.SqsUrl,
                UseHttp = true
            });
            
            container.RegisterInstanceAs<IAmazonSQS>(sqsClient);

            container.RegisterTypeAs<TokenProvider, ITokenProvider>();
            container.RegisterTypeAs<UserService, UserService>();
            container.RegisterTypeAs<SqsService, SqsService>();
            container.RegisterFactoryAs<UsersApiClient>(c => new UsersApiClient(ApiConfig.ApiBaseUrl, c.Resolve<ITokenProvider>()));
        }
    }
}