using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiAndUiProject.UI.Pages;

namespace ApiAndUiProject.UI.Helpers
{

    public interface IPageFactory
    {
        T Create<T>() where T : BasePage;
        BasePage CreatePageByName(string pageName);
    }
}