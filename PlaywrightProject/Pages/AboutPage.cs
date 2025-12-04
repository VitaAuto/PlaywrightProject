using Microsoft.Playwright;
using PlaywrightProject.Attributes;
using PlaywrightProject.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;

namespace PlaywrightProject.Pages
{
    public class AboutPage(IPage page) : BasePage(page)
    {
        public override string Url => "https://www.epam.com/about";
        public HeaderComponent Header => new HeaderComponent(Page);
    }
}
