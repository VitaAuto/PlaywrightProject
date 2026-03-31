using ApiAndUiProject.UI.Attributes;
using Microsoft.Playwright;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiAndUiProject.UI.Components
{
    public class HamburgerMenuComponent(IPage page) : BaseComponent(page, page.Locator("nav.hamburger-menu__dropdown"))
    {
        [Name("Hamburger Menu Services Link")]
        public BaseButton HamburgerMenuServicesLink => new BaseButton(Page, Page.GetByRole(AriaRole.Link, new() { Name = "Services" }).First);

        [Name("Hamburger Menu Industries Link")]
        public BaseButton HamburgerMenuIndustriesLink => new BaseButton(Page, Page.GetByText("Industries").First);

        [Name("Hamburger Menu Insights Link")]
        public BaseButton HamburgerMenuInsightsLink => new BaseButton(Page, Page.GetByRole(AriaRole.Link, new() { Name = "Insights" }).First);

        [Name("Hamburger Menu About Link")]
        public BaseButton HamburgerMenuAboutLink => new BaseButton(Page, Page.GetByRole(AriaRole.Link, new() { Name = "About" }).First);

        [Name("Hamburger Menu Careers Link")]
        public BaseButton HamburgerMenuCareersLink => new BaseButton(Page, Page.GetByRole(AriaRole.Link, new() { Name = "Careers" }).First);

        public override ILocator OptionsLocator => Locator.Locator("//*[contains(@class, 'hamburger-menu__item item') and contains(@class, 'item')]");
    }
}