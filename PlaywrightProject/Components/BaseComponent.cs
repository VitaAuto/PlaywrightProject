using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlaywrightProject.Components
{
    public abstract class BaseComponent
    {
        protected readonly IPage Page;
        protected readonly ILocator Locator;

        protected BaseComponent(IPage page, ILocator locator)
        {
            Page = page;
            Locator = locator;
        }

        public async Task<bool> IsVisibleAsync() => await Locator.IsVisibleAsync();
        public async Task<string> GetTextAsync() => await Locator.InnerTextAsync();
        public async Task HoverAsync() => await Locator.HoverAsync();

        /// Универсальный метод: получить текст любого сообщения по CSS-селектору
        public async Task<string?> GetMessageTextAsync(string cssSelector)
        {
            var locator = Page.Locator(cssSelector);
            if (await locator.IsVisibleAsync())
                return await locator.InnerTextAsync();
            return null;
        }

        /// Универсальный метод: проверить видимость любого сообщения по CSS-селектору
        public async Task<bool> IsMessageVisibleAsync(string cssSelector)
        {
            var locator = Page.Locator(cssSelector);
            return await locator.IsVisibleAsync();
        }
    }
}