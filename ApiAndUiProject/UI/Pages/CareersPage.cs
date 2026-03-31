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
        public HeaderComponent Header => new (Page);
        
        [Name("Cookie Consent popup")]
        public CookieConsentComponent CookieConsentPopup => new(Page);

        [Name("Start Your Search Here Button")]
        public BaseButton StartYourSearchHere => new (Page, Page.Locator("#main").GetByRole(AriaRole.Link, new() { Name = "Start Your Search Here" }));
    }
}
