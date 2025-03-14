using Moq;
using Domain;
using System;
using Xunit;

namespace Tests
{
    public class EventHandlerTests
    {
        private readonly Mock<IEventDatabaseInteractor> _mockEventInteractor;
        private readonly Mock<ICategoryDatabaseInteractor> _mockCategoryInteractor;
        private readonly Mock<IUserDatabaseInteractor> _mockUserInteractor;
        private readonly EventModule _eventModule;
        
        public EventHandlerTests()
        {
            _mockEventInteractor = new Mock<IEventDatabaseInteractor>();
            _mockCategoryInteractor = new Mock<ICategoryDatabaseInteractor>();
            _mockUserInteractor = new Mock<IUserDatabaseInteractor>();

            Interactors.EventInteractor = _mockEventInteractor.Object;
            Interactors.CategoryInteractor = _mockCategoryInteractor.Object;
            Interactors.UserInteractor = _mockUserInteractor.Object;

            _eventModule = new EventModule();
        }

        [Fact]
        public void OneTest()
        {
            var e = new Event { Id = 1, Title = "Test"};
            _mockEventInteractor.Setup(m => m.Add("Test")).Returns(e);

            var result = _eventModule.AddEvent("Test");

            Assert.Equal(1, result.Id);
            _mockEventInteractor.Verify(m => m.Add("Test"), Times.Once);
        }
    }
}
