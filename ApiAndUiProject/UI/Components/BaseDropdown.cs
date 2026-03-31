using Microsoft.Playwright;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiAndUiProject.UI.Components
{
    public class BaseDropdown : BaseComponent
    {
        public BaseDropdown(IPage page, ILocator locator) : base(page, locator ) { }
        
        public override async Task<bool> IsVisibleAsync() => await Locator.IsVisibleAsync();
    }
}