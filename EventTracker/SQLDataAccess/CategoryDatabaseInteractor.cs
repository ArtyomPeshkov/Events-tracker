using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace SQLDataAccess
{
    public class CategoryDatabaseInteractor : ICategoryDatabaseInteractor
    {
        private readonly IDatabase _database;

        public CategoryDatabaseInteractor(IDatabase database)
        {
            _database = database;
        }

        public Category Add(Category categoryItem)
        {
            return _database.Add("Categorys", categoryItem);
        }

        public Category Update(long id, Category categoryItem)
        {
            return _database.Update("Categorys", id, categoryItem);
        }

        public void Delete(long categoryId)
        {
            _database.Delete<Category>("Categorys", categoryId);
        }

        public Category Get(long categoryId)
        {
            return _database.Get<Category>("Categorys", categoryId);
        }
    }
}
