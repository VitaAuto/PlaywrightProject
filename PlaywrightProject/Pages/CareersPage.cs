using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;
using System.Threading.Tasks;
using PlaywrightProject.Attributes;

namespace PlaywrightProject.Pages
{
    public class CareersPage(IPage page) : BasePage(page)
    {
        public override string Url => "https://www.epam.com/careers";

        [Name("Contact Us Button")]
        public async Task<IElementHandle?> GetContactUsTextSpanAsync() =>
            await Page.QuerySelectorAsync("(//div[contains(@class, 'cta-button-wrapper')]//a[@href='https://www.epam.com/about/who-we-are/contact']//span[contains(text(), 'CONTACT US')])[1]");
    }
}
