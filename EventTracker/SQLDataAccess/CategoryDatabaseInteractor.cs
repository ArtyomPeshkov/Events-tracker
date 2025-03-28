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
            return _database.AddCategory(categoryItem);
        }

        public Category Update(long id, Category categoryItem)
        {
            return _database.UpdateCategory(id, categoryItem);
        }

        public void Delete(long categoryId)
        {
            _database.DeleteCategory(categoryId);
        }

        public Category Get(long categoryId)
        {
            return _database.GetCategory(categoryId);
        }

        public Category GetByName(string title)
        {
            return _database.GetCategoryByName(title);
        }
    }
}
