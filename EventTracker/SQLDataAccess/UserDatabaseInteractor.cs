using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDataAccess
{
    public class UserDatabaseInteractor : IUserDatabaseInteractor
    {
        private readonly IDatabase _database;

        public UserDatabaseInteractor(IDatabase database)
        {
            _database = database;
        }

        public User Add(User user)
        {
            return _database.Add<User>("Users", user);
        }

        public User Get(long userId)
        {
            return _database.Get<User>("Users", userId);
        }

        public User? GetByLogin(string login)
        {
            return _database.GetByLogin(login);
        }
    }

}
