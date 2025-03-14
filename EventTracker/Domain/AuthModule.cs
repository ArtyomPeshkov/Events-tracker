using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AuthModule : IAuthModule
    {
        private readonly IUserDatabaseInteractor _userInteractor;

        public AuthModule()
        {
            _userInteractor = Interactors.UserInteractor;
        }

        public User SignUp(string login, string password)
        {
            var existingUser = _userInteractor.GetByLogin(login);
            if (existingUser != null)
            {
                throw new Exception("User  already exists.");
            }

            var user = new User { Login = login, PasswordHash = HashPassword(password) };
            return _userInteractor.Add(user);
        }

        public User SignIn(string login, string password)
        {
            var user = _userInteractor.GetByLogin(login);
            if (user == null || !VerifyPassword(password, user.PasswordHash))
            {
                throw new Exception("Invalid login or password.");
            }

            return user;
        }

        private string HashPassword(string password)
        {
            // Simple hash function for demonstration purposes
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return HashPassword(password) == hashedPassword;
        }
    }

}
