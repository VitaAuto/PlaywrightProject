using Microsoft.Playwright;
using PlaywrightProject.UI.Attributes;

namespace PlaywrightProject.UI.Components
{
    public class FooterComponent : BaseComponent
    {
        public FooterComponent(IPage page): base(page, page.Locator("footer")) { }

        [Name("Cookie Policy Link")]
        public BaseButton CookiePolicyLink => new BaseButton(Page, Page.Locator("a[href*='cookie-policy']"));

        [Name("Privacy Policy Link")]
        public BaseButton PrivacyPolicyLink => new BaseButton(Page, Page.Locator("a[href*='privacy-policy']"));

        [Name("LinkedIn Link")]
        public BaseButton LinkedInLink => new BaseButton(Page, Page.Locator("a[href*='linkedin.com']"));

        [Name("Facebook Link")]
        public BaseButton FacebookLink => new BaseButton(Page, Page.Locator("a[href*='facebook.com']"));

        [Name("YouTube Link")]
        public BaseButton YouTubeLink => new BaseButton(Page, Page.Locator("a[href*='youtube.com']"));

        // Добавь другие элементы футера по необходимости
    }
}