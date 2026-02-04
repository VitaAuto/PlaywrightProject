using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlaywrightProject.UI.Components
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

        public virtual async Task<bool> IsVisibleAsync() => await Locator.IsVisibleAsync();
        public async Task<string> GetTextAsync() => await Locator.InnerTextAsync();
        public async Task HoverAsync() => await Locator.HoverAsync();
        public async Task WaitForVisibleAsync(int timeoutMs = 5000)
        {
            await Locator.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = timeoutMs
            });
        }
        public async Task WaitForHiddenAsync(int timeoutMs = 5000)
        {
            await Locator.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Hidden,
                Timeout = timeoutMs
            });
        }
    }
}