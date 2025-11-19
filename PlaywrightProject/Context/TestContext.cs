using Microsoft.Playwright;
using PlaywrightProject.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightProject.Context
{
    public class TestContext
    {
        public IPage? Page { get; set; }
        public BasePage? CurrentPage { get; set; }
    }
}
