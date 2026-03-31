using Microsoft.Playwright;
using ApiAndUiProject.UI.Attributes;

namespace ApiAndUiProject.UI.Components
{
    public class HeaderComponent : BaseComponent
    {
        public HeaderComponent(IPage page) : base(page, page.Locator("header.header-ui-23")) { }

        [Name("Hamburger Menu Button")]
        public BaseButton HamburgerMenuButton => new (Page, Page.GetByRole(AriaRole.Button).First); 
        
        [Name("Hamburger Menu")]
        public HamburgerMenuComponent HamburgerMenu => new (Page);

        [Name("Search Button")]
        public BaseButton SearchButton => new (Page, Page.Locator("button.header-search__button"));

        [Name("Search Input")]
        public BaseTextField SearchInput => new (Page, Page.Locator("input.header-search__input#new_form_search"));

        [Name("Search Results")]
        public SearchResultsComponent SearchResults => new (Page);

        [Name("Find Button")]
        public BaseButton FindButton => new (Page, Page.Locator("div.search-results__action-section > button"));

        [Name("Contact Us Button")]
        public BaseButton ContactUsButton => new (Page, Page.Locator("(//div[contains(@class, 'cta-button-wrapper')]//a[@href='https://www.epam.com/about/who-we-are/contact']//span[contains(text(), 'CONTACT US')])[2]"));

    }
}