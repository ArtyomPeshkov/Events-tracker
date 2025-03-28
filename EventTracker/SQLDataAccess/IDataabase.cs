using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDataAccess
{
    public interface IDatabase
    {
        Event AddEvent(Event newEvent, List<long> categoryIds = null);
        Event UpdateEvent(long id, Event updatedEvent, List<long> categoryIds = null);
        void DeleteEvent(long id);
        Event GetEvent(long id);

        User AddUser(User newUser);
        User UpdateUser(long id, User updatedUser);
        void DeleteUser(long id);
        User GetUser(long id);

        Category AddCategory(Category newCategory);
        Category UpdateCategory(long id, Category updatedCategory);
        void DeleteCategory(long id);
        Category GetCategory(long id);

        User? GetByLogin(string login);
        Category? GetCategoryByName(string title);
    }

}
