using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Delegates;
using Common.Structures;

namespace Common.Interfaces
{
    public interface IAuthenticationProvider
    {
        AuthResult Authenticate(string UserName, string Password);
    }
}
