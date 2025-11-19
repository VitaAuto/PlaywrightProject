using Microsoft.Playwright;
using PlaywrightProject.Attributes;

namespace PlaywrightProject.Components
{
    public class HeaderComponent : BaseComponent
    {
        public HeaderComponent(IPage page) : base(page, page.Locator("header")) { }

        [Name("Hamburger Menu Button")]
        public BaseButton HamburgerMenuButton => new BaseButton(Page, Page.Locator("button.hamburger-menu__button"));

        [Name("Hamburger Menu Services Link")]
        public BaseButton HamburgerMenuServicesLink => new BaseButton(Page, Page.Locator("a.hamburger-menu__link.first-level-link.gradient-text[href='/services']:first-of-type"));

        [Name("Search Button")]
        public BaseButton SearchButton => new BaseButton(Page, Page.Locator("button.header-search__button"));

        [Name("Search Input")]
        public BaseTextField SearchInput => new BaseTextField(Page, Page.Locator("#new_form_search"));

        [Name("Search Results")]
        public SearchResultsComponent SearchResults => new SearchResultsComponent(Page);

        [Name("Find Button")]
        public BaseButton FindButton => new BaseButton(Page, Page.Locator("div.search-results__action-section > button"));

        [Name("Contact Us Button")]
        public BaseButton ContactUsButton => new BaseButton(Page, Page.Locator("(//div[contains(@class, 'cta-button-wrapper')]//a[@href='https://www.epam.com/about/who-we-are/contact']//span[contains(text(), 'CONTACT US')])[2]"));

    }
}