using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlaywrightProject.UI.Components
{
    public class BaseButton : BaseComponent
    {
        public BaseButton(IPage page, ILocator locator) : base(page, locator) { }

        public async Task ClickAsync() => await Locator.ClickAsync();
        public async Task<bool> IsEnabledAsync() => await Locator.IsEnabledAsync();
        public async Task<bool> IsDisabledAsync() => !await Locator.IsEnabledAsync();
        public async Task<string> GetBackgroundColorAsync() =>
            await Locator.EvaluateAsync<string>("el => getComputedStyle(el).backgroundColor");
        public async Task<string> GetTextColorAsync() =>
            await Locator.EvaluateAsync<string>("el => getComputedStyle(el).color");
        public async Task<string> GetBorderColorAsync() =>
            await Locator.EvaluateAsync<string>("el => getComputedStyle(el).borderColor");
        public async Task<string> GetCursorAsync() =>
            await Locator.EvaluateAsync<string>("el => getComputedStyle(el).cursor");
        public async Task<bool> IsBackgroundColorChangedOnHoverAsync()
        {
            var beforeHover = await GetBackgroundColorAsync();
            await HoverAsync();
            var afterHover = await GetBackgroundColorAsync();
            return beforeHover != afterHover;
        }

        public async Task<int> GetCountAsync() => await Locator.CountAsync();
        public async Task<bool> GetIsVisibleAsync() => await Locator.IsVisibleAsync();
    }
}