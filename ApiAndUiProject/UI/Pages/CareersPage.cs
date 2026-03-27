using Microsoft.Playwright;
using ApiAndUiProject.UI.Attributes;
using ApiAndUiProject.UI.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;

namespace ApiAndUiProject.UI.Pages
{
    public class CareersPage(IPage page) : BasePage(page)
    {
        public override string Url => "https://www.epam.com/careers";
        public HeaderComponent Header => new HeaderComponent(Page);
    }
}
