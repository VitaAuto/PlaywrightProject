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
    public class ServicesPage(IPage page) : BasePage(page)
    {
        public override string Url => "https://www.epam.com/services";
        public HeaderComponent Header => new HeaderComponent(Page);
    }
}
