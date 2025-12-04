using PlaywrightProject.Pages;
using Microsoft.Playwright;
using System;

namespace PlaywrightProject.Helpers
{
    public static class PageFactory
    {
        public static BasePage CreatePageByName(string pageName, IPage playwrightPage)
        {
            return pageName.ToLower() switch
            {
                "main" => new MainPage(playwrightPage),
                "services" => new ServicesPage(playwrightPage),
                "insights" => new InsightsPage(playwrightPage),
                "about" => new AboutPage(playwrightPage),
                "careers" => new CareersPage(playwrightPage),
                _ => throw new ArgumentException($"Page '{pageName}' is not defined.")
            };
        }
    }
}