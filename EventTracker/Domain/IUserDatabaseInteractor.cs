using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IUserDatabaseInteractor
    {
        User Add(User user);
        User Get(long userId);

        User? GetByLogin(string login);
    }
}
