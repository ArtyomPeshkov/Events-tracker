using Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDataAccess
{
    public class Database : IDatabase
    {
        private readonly Dictionary<string, List<object>> _tables = new Dictionary<string, List<object>>();
        private Random random = new Random();

        public T Add<T>(string tableName, T item)
        {
            if (!_tables.ContainsKey(tableName))
            {
                _tables[tableName] = new List<object>();
            }
            
            var list = _tables[tableName];
            var id = random.Next();

            var existingItem = list.FirstOrDefault(i => (i as dynamic).Id == (item as dynamic).Id);
            while (existingItem != null)
            {
                id = random.Next();
                existingItem = list.FirstOrDefault(i => (i as dynamic).Id == (item as dynamic).Id);
            }

            (item as dynamic).Id = id;
;
            _tables[tableName].Add(item);
            return item;
        }

        public T Update<T>(string tableName, long id, T item)
        {
            if (!_tables.ContainsKey(tableName))
            {
                _tables[tableName] = new List<object>();
            }

            var list = _tables[tableName];
            var existingItem = list.FirstOrDefault(i => (i as dynamic).Id == id);
            if (existingItem == null)
            {
                return Add(tableName, item);
            }
            // TODO: implement
            return item;
        }

        public void Delete<T>(string tableName, long id)
        {
            if (!_tables.ContainsKey(tableName))
            {
                _tables[tableName] = new List<object>();
            }
            var list = _tables[tableName];
            var itemToRemove = list.FirstOrDefault(i => (i as dynamic).Id == id);
            if (itemToRemove != null)
            {
                list.Remove(itemToRemove);
            }
        }

        public T Get<T>(string tableName, long id)
        {
            if (_tables.ContainsKey(tableName))
            {
                return (T)_tables[tableName].FirstOrDefault(i => (i as dynamic).Id == id);
            }
            return default;
        }

        public User? GetByLogin(string login)
        {
            if (_tables.ContainsKey("Users"))
            {
                return (User?)_tables["Users"].FirstOrDefault(i => ((i as dynamic).login == login), null);
            }
            return null;
        }
    }
}
