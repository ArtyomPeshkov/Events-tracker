using Domain;
using Microsoft.EntityFrameworkCore;

namespace SQLDataAccess
{
    public class Database : IDatabase
    {
        private readonly EventDBContext _context;

        public Database(EventDBContext context)
        {
            _context = context;
            Recreate();
        }

        public void Drop()
        {
            _context.Database.EnsureDeleted();
        }

        public void Recreate()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        public Event AddEvent(Event newEvent, List<long> categoryIds = null)
        {
            _context.EventEntities.Add(newEvent);
            _context.SaveChanges();

            if (categoryIds != null)
            {
                foreach (var categoryId in categoryIds)
                {
                    _context.EventCategories.Add(new EventCategory
                    {
                        EventId = newEvent.Id,
                        CategoryId = categoryId
                    });
                }
                _context.SaveChanges();
            }

            return newEvent;
        }

        public Event UpdateEvent(long id, Event updatedEvent, List<long> categoryIds = null)
        {
            var existingEvent = _context.EventEntities.Include(e => e.EventCategories).FirstOrDefault(e => e.Id == id);
            if (existingEvent != null)
            {
                existingEvent.Title = updatedEvent.Title;
                existingEvent.Description = updatedEvent.Description;
                // TODO: Update other properties as needed

                if (categoryIds != null)
                {
                    existingEvent.EventCategories.Clear();
                    foreach (var categoryId in categoryIds)
                    {
                        existingEvent.EventCategories.Add(new EventCategory
                        {
                            EventId = existingEvent.Id,
                            CategoryId = categoryId
                        });
                    }
                }

                _context.SaveChanges();
            }
            return existingEvent;
        }

        public void DeleteEvent(long id)
        {
            var eventToDelete = _context.EventEntities.Include(e => e.EventCategories).FirstOrDefault(e => e.Id == id);
            if (eventToDelete != null)
            {
                _context.EventCategories.RemoveRange(eventToDelete.EventCategories);
                _context.EventEntities.Remove(eventToDelete);
                _context.SaveChanges();
            }
        }

        public Event GetEvent(long id)
        {
            return _context.EventEntities.Find(id);
        }

        public User AddUser(User newUser)
        {
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return newUser;
        }

        public User UpdateUser(long id, User updatedUser)
        {
            var existingUser = _context.Users.Find(id);
            if (existingUser != null)
            {
                existingUser.Login = updatedUser.Login;
                existingUser.PasswordHash = updatedUser.PasswordHash;
                // TODO: Update other properties as needed
                _context.SaveChanges();
            }
            return existingUser;
        }

        public void DeleteUser(long id)
        {
            var userToDelete = _context.Users.Find(id);
            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
                _context.SaveChanges();
            }
        }

        public User GetUser(long id)
        {
            return _context.Users.Find(id);
        }

        public Category AddCategory(Category newCategory)
        {
            _context.Categories.Add(newCategory);
            _context.SaveChanges();
            return newCategory;
        }

        public Category UpdateCategory(long id, Category updatedCategory)
        {
            var existingCategory = _context.Categories.Find(id);
            if (existingCategory != null)
            {
                existingCategory.Title = updatedCategory.Title;
                // TODO: Update other properties as needed
                _context.SaveChanges();
            }
            return existingCategory;
        }

        public void DeleteCategory(long id)
        {
            var categoryToDelete = _context.Categories.Include(c => c.EventCategories).FirstOrDefault(c => c.Id == id);
            if (categoryToDelete != null)
            {
                _context.EventCategories.RemoveRange(categoryToDelete.EventCategories);
                _context.Categories.Remove(categoryToDelete);
                _context.SaveChanges();
            }
        }

        public Category GetCategory(long id)
        {
            return _context.Categories.Find(id);
        }

        public User? GetByLogin(string login)
        {
            return _context.Users.SingleOrDefault(u => u.Login == login);
        }

        public Category? GetCategoryByName(string title)
        {
            return _context.Categories.SingleOrDefault(c => c.Title == title);
        }
    }
}
