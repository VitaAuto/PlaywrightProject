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
    public class ServicesPage(IPage page) : BasePage(page)
    {
        public override string Url => "https://www.epam.com/services";
        public HeaderComponent Header => new HeaderComponent(Page);

        [Name("Cookie Consent popup")]
        public CookieConsentComponent CookieConsentPopup => new(Page);
    }
}
