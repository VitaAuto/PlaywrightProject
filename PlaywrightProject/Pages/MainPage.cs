using Microsoft.Playwright;
using PlaywrightProject.Components;

namespace PlaywrightProject.Pages
{
    public class MainPage : BasePage
    {
        public MainPage(IPage page) : base(page) { }

        public override string Url => "https://www.epam.com";
        public HeaderComponent Header => new HeaderComponent(Page);
        public FooterComponent Footer => new FooterComponent(Page);

        // Уникальные элементы только для главной страницы (если есть)
    }
}