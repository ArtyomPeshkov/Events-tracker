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
        T Add<T>(string tableName, T item);
        T Update<T>(string tableName, long id, T item);
        void Delete<T>(string tableName, long id);
        T Get<T>(string tableName, long id);
        User? GetByLogin(string login);
    }
}
