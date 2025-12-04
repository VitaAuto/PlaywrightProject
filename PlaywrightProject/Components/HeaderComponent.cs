using Microsoft.Playwright;
using PlaywrightProject.Attributes;

namespace PlaywrightProject.Components
{
    public class HeaderComponent : BaseComponent
    {
        public HeaderComponent(IPage page) : base(page, page.Locator("header.header-ui-23")) { }

        [Name("Hamburger Menu Button")]
        public BaseButton HamburgerMenuButton => new BaseButton(Page, Page.Locator("button.hamburger-menu__button"));

        [Name("Hamburger Menu Services Link")]
        public BaseButton HamburgerMenuServicesLink => new BaseButton(Page, Page.Locator("a.hamburger-menu__link.first-level-link.gradient-text[href='/services']:first-of-type"));

        [Name("Hamburger Menu Industries Link")]
        public BaseButton HamburgerMenuIndustriesLink => new BaseButton(Page, Page.Locator("a.hamburger-menu__link.first-level-link.gradient-text:not(.hidden-underline-node)").Filter(new() { HasText = "Industries" }));

        [Name("Hamburger Menu Insights Link")]
        public BaseButton HamburgerMenuInsightsLink => new BaseButton(Page, Page.Locator("a.hamburger-menu__link.first-level-link.gradient-text[href='/insights']:not(.hidden-underline-node)"));

        [Name("Hamburger Menu About Link")]
        public BaseButton HamburgerMenuAboutLink => new BaseButton(Page, Page.Locator("a.hamburger-menu__link.first-level-link.gradient-text[href='/about']:not(.hidden-underline-node)"));

        [Name("Hamburger Menu Careers Link")]
        public BaseButton HamburgerMenuCareersLink => new BaseButton(Page, Page.Locator("a.hamburger-menu__link.first-level-link.gradient-text[href='/careers']:not(.hidden-underline-node)"));

        [Name("Search Button")]
        public BaseButton SearchButton => new BaseButton(Page, Page.Locator("button.header-search__button"));

        [Name("Search Input")]
        public BaseTextField SearchInput => new BaseTextField(Page, Page.Locator("input.header-search__input#new_form_search"));

        [Name("Search Results")]
        public SearchResultsComponent SearchResults => new SearchResultsComponent(Page);

        [Name("Find Button")]
        public BaseButton FindButton => new BaseButton(Page, Page.Locator("div.search-results__action-section > button"));

        [Name("Contact Us Button")]
        public BaseButton ContactUsButton => new BaseButton(Page, Page.Locator("(//div[contains(@class, 'cta-button-wrapper')]//a[@href='https://www.epam.com/about/who-we-are/contact']//span[contains(text(), 'CONTACT US')])[2]"));

    }
}