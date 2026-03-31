using ApiAndUiProject.UI.Attributes;
using ApiAndUiProject.UI.Components;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading.Tasks;

namespace ApiAndUiProject.UI.Pages
{
    public class CareersJobsPage(IPage page) : BasePage(page)
    {
        public override string Url => "https://careers.epam.com/";
        
        [Name("Country Dropdown Clear")]
        public BaseDropdown CountryLocationClear => new (Page, Page.Locator("div[class*='dropdown__clear-indicator']").First);

        [Name("Country Dropdown Default Placeholder")]
        public BaseDropdown CountryDropdownDefaultPlaceholder => new(Page, Page.Locator("div.dropdown__placeholder").Filter(new() { HasText = "Choose your country" })
);

    }
}
