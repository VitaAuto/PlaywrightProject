using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiAndUiProject.API.Auth
{
    public interface ITokenProvider
    {
        string? GetToken();
        void SetToken(string token);
    }
}
