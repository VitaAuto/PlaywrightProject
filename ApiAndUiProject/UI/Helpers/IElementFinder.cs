using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiAndUiProject.UI.Helpers
{
    public interface IElementFinder
    {
        object? FindElementByName(object pageObject, string name);
    }
}
