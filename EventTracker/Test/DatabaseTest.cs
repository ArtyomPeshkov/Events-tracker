using Domain;
using Microsoft.EntityFrameworkCore;
using SQLDataAccess;

namespace Test
{
    public class TestEventContext : EventDBContext
    {

        public User user1 = new User { Id = 1, Login = "First", PasswordHash = "Hash1" };
        public User user2 = new User { Id = 2, Login = "Second", PasswordHash = "Hash2" };
        public User user3 = new User { Id = 3, Login = "Third", PasswordHash = "Hash3" };

        public Profile profile1 = new Profile { Id = 1, FirstName = "Name1", LastName = "Last1", UserId = 1 };
        public Profile profile2 = new Profile { Id = 2, FirstName = "Name1", LastName = "Last1", UserId = 2 };

        public Category category1 = new Category { Id = 1, Title = "Cat_1" };
        public Category category2 = new Category { Id = 2, Title = "Cat_2" };

        public Event event1 = new Event { Id = 1, Title = "Event_1", Description = "", UserId = 1 };
        public Event event2 = new Event { Id = 2, Title = "Event_2", Description = "", UserId = 1 };
        public Event event3 = new Event { Id = 3, Title = "Event_3", Description = "", UserId = 2 };
        public Event event4 = new Event { Id = 4, Title = "Event_4", Description = "", UserId = 3 };
        public Event event5 = new Event { Id = 5, Title = "Event_5", Description = "", UserId = 3 };
        public TestEventContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Event>().HasData(event1, event2, event3, event4, event5);

            modelBuilder.Entity<User>().HasData(user1, user2, user3);

            modelBuilder.Entity<Category>().HasData(category1, category2);

            modelBuilder.Entity<Profile>().HasData(profile1, profile2);
        }
    }
    public class DatabaseTests
    {
        private DbContextOptions<EventDBContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<EventDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }


        [Fact]
        public void GetPreloadedData_User()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using var context = new TestEventContext(options);
            var database = new Database(context);

            // Act
            var user1 = database.GetUser(context.user1.Id);
            var user2 = database.GetUser(context.user2.Id);
            var user3 = database.GetUser(context.user3.Id);

            // Assert
            Assert.NotNull(user1);
            Assert.NotNull(user2);
            Assert.NotNull(user3);
            Assert.Equal(context.user1.Login, user1.Login);
            Assert.Equal(context.user2.Login, user2.Login);
            Assert.Equal(context.user3.Login, user3.Login);
        }


        public void GetPreloadedData_Event()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using var context = new TestEventContext(options);
            var database = new Database(context);

            // Act
            var event1 = database.GetEvent(context.event1.Id);
            var event2 = database.GetEvent(context.event2.Id);
            var event3 = database.GetEvent(context.event3.Id);
            var event4 = database.GetEvent(context.event4.Id);
            var event5 = database.GetEvent(context.event5.Id);

            // Assert
            Assert.NotNull(event1);
            Assert.NotNull(event2);
            Assert.NotNull(event3);
            Assert.NotNull(event4);
            Assert.NotNull(event5);
            Assert.Equal(context.event1.Title, event1.Title);
            Assert.Equal(context.event2.Title, event2.Title);
            Assert.Equal(context.event3.Title, event3.Title);
            Assert.Equal(context.event4.Title, event4.Title);
            Assert.Equal(context.event5.Title, event5.Title);
        }

        public void GetPreloadedData_Category()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using var context = new TestEventContext(options);
            var database = new Database(context);

            // Act
            var category1 = database.GetCategory(context.category1.Id);
            var category2 = database.GetCategory(context.category2.Id);

            // Assert
            Assert.NotNull(category1);
            Assert.NotNull(category2);
            Assert.Equal(context.category1.Title, category1.Title);
            Assert.Equal(context.category2.Title, category2.Title);
        }

        [Fact]
        public void AddUser_ShouldAddUser()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using var context = new TestEventContext(options);
            var database = new Database(context);
            var user = new User { Login = "TestUser ", PasswordHash = "TestHash" };

            // Act
            var addedUser = database.AddUser(user);

            // Assert
            Assert.NotNull(addedUser);
            Assert.Equal(user.Login, addedUser.Login);
        }

        [Fact]
        public void UpdateUser_ShouldUpdateUser()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using var context = new TestEventContext(options);
            var database = new Database(context);
            var user = new User { Login = "TestUser ", PasswordHash = "TestHash" };
            var addedUser = database.AddUser(user);
            var updatedUser = new User { Login = "UpdatedUser ", PasswordHash = "UpdatedHash" };

            // Act
            var result = database.UpdateUser(addedUser.Id, updatedUser);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedUser.Login, result.Login);
        }

        [Fact]
        public void DeleteUser_ShouldDeleteUser()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using var context = new TestEventContext(options);
            var database = new Database(context);
            var user = new User { Login = "TestUser ", PasswordHash = "TestHash" };
            var addedUser = database.AddUser(user);

            // Act
            database.DeleteUser(addedUser.Id);

            // Assert
            var deletedUser = database.GetUser(addedUser.Id);
            Assert.Null(deletedUser);
        }

        [Fact]
        public void AddEvent_ShouldAddEvent()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using var context = new TestEventContext(options);
            var database = new Database(context);
            var user = new User { Login = "TestUser ", PasswordHash = "TestHash" };
            var addedUser = database.AddUser(user);
            var newEvent = new Event { Title = "Test Event", Description = "Test Description", UserId = addedUser.Id };

            // Act
            var addedEvent = database.AddEvent(newEvent);

            // Assert
            Assert.NotNull(addedEvent);
            Assert.Equal(newEvent.Title, addedEvent.Title);
        }

        [Fact]
        public void UpdateEvent_ShouldUpdateEvent()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using var context = new TestEventContext(options);
            var database = new Database(context);
            var user = new User { Login = "TestUser ", PasswordHash = "TestHash" };
            var addedUser = database.AddUser(user);
            var newEvent = new Event { Title = "Test Event", Description = "Test Description", UserId = addedUser.Id };
            var addedEvent = database.AddEvent(newEvent);
            var updatedEvent = new Event { Title = "Updated Event", Description = "Updated Description" };

            // Act
            var result = database.UpdateEvent(addedEvent.Id, updatedEvent);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedEvent.Title, result.Title);
        }

        [Fact]
        public void DeleteEvent_ShouldDeleteEvent()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using var context = new TestEventContext(options);
            var database = new Database(context);
            var user = new User { Login = "TestUser ", PasswordHash = "TestHash" };
            var addedUser = database.AddUser(user);
            var newEvent = new Event { Title = "Test Event", Description = "Test Description", UserId = addedUser.Id };
            var addedEvent = database.AddEvent(newEvent);

            // Act
            database.DeleteEvent(addedEvent.Id);

            // Assert
            var deletedEvent = database.GetEvent(addedEvent.Id);
            Assert.Null(deletedEvent);
        }

        [Fact]
        public void AddCategory_ShouldAddCategory()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using var context = new TestEventContext(options);
            var database = new Database(context);
            var newCategory = new Category { Title = "Test Category" };

            // Act
            var addedCategory = database.AddCategory(newCategory);

            // Assert
            Assert.NotNull(addedCategory);
            Assert.Equal(newCategory.Title, addedCategory.Title);
        }

        [Fact]
        public void UpdateCategory_ShouldUpdateCategory()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using var context = new TestEventContext(options);
            var database = new Database(context);
            var newCategory = new Category { Title = "Test Category" };
            var addedCategory = database.AddCategory(newCategory);
            var updatedCategory = new Category { Title = "Updated Category" };

            // Act
            var result = database.UpdateCategory(addedCategory.Id, updatedCategory);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedCategory.Title, result.Title);
        }

        [Fact]
        public void DeleteCategory_ShouldDeleteCategory()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using var context = new TestEventContext(options);
            var database = new Database(context);
            var newCategory = new Category { Title = "Test Category" };
            var addedCategory = database.AddCategory(newCategory);

            // Act
            database.DeleteCategory(addedCategory.Id);

            // Assert
            var deletedCategory = database.GetCategory(addedCategory.Id);
            Assert.Null(deletedCategory);
        }

        [Fact]
        public void GetUser_ShouldReturnUser()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using var context = new TestEventContext(options);
            var database = new Database(context);
            var user = new User { Login = "TestUser ", PasswordHash = "TestHash" };
            var addedUser = database.AddUser(user);

            // Act
            var retrievedUser = database.GetUser(addedUser.Id);

            // Assert
            Assert.NotNull(retrievedUser);
            Assert.Equal(addedUser.Login, retrievedUser.Login);
        }

        [Fact]
        public void GetEvent_ShouldReturnEvent()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using var context = new TestEventContext(options);
            var database = new Database(context);
            var user = new User { Login = "TestUser ", PasswordHash = "TestHash" };
            var addedUser = database.AddUser(user);
            var newEvent = new Event { Title = "Test Event", Description = "Test Description", UserId = addedUser.Id };
            var addedEvent = database.AddEvent(newEvent);

            // Act
            var retrievedEvent = database.GetEvent(addedEvent.Id);

            // Assert
            Assert.NotNull(retrievedEvent);
            Assert.Equal(addedEvent.Title, retrievedEvent.Title);
        }

        [Fact]
        public void GetCategory_ShouldReturnCategory()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using var context = new TestEventContext(options);
            var database = new Database(context);
            var newCategory = new Category { Title = "Test Category" };
            var addedCategory = database.AddCategory(newCategory);

            // Act
            var retrievedCategory = database.GetCategory(addedCategory.Id);

            // Assert
            Assert.NotNull(retrievedCategory);
            Assert.Equal(addedCategory.Title, retrievedCategory.Title);
        }
    }
}

