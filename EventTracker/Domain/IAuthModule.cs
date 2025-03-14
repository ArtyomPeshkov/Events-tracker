using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IAuthModule
    {
        User SignUp(string login, string password);
        User SignIn(string login, string password);
    }
}
