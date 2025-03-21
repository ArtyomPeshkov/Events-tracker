using Moq;
using Domain;
using System;
using Xunit;
using Castle.Components.DictionaryAdapter.Xml;

namespace Tests
{
    public class EventHandlerTests
    {
        private readonly Mock<IEventDatabaseInteractor> _mockEventInteractor;
        private readonly Mock<ICategoryDatabaseInteractor> _stubCategoryInteractor;
        private readonly Mock<IUserDatabaseInteractor> _stubUserInteractor;
        private readonly EventModule _eventModule;
        private Dictionary<long, Event> _events = new Dictionary<long, Event>();

        private void DefaultSetupMock()
        {
            _mockEventInteractor.Setup(m => m.Add(It.IsAny<Event?>()))
                .Callback<Event?>(eventItem =>
                {
                    _events[eventItem.Id] = eventItem;
                })
                .Returns<Event?>(eventItem => _events[eventItem.Id]);


            _mockEventInteractor.Setup(m => m.Delete(It.IsAny<long>()))
                .Callback<long>(eventId =>
                {
                    _events.Remove(eventId);
                });

            _mockEventInteractor.Setup(m => m.Update(It.IsAny<long>(), It.IsAny<Event?>()))
                .Callback<long, Event?>((eventId, eventItem) =>
                {
                    _events[eventId] = eventItem;
                })
                .Returns<long, Event?>((eventId, _) => _events[eventId]);

            _mockEventInteractor.Setup(m => m.Get(It.IsAny<long>()))
                .Returns<long>(eventId => _events[eventId]);


        }

        public EventHandlerTests()
        {
            _mockEventInteractor = new Mock<IEventDatabaseInteractor>();
            _stubCategoryInteractor = new Mock<ICategoryDatabaseInteractor>();
            _stubUserInteractor = new Mock<IUserDatabaseInteractor>();
            DefaultSetupMock();

            _eventModule = new EventModule(_mockEventInteractor.Object);
        }

        [Fact]
        public void CoreAddTest()
        {

            var result = _eventModule.AddEvent("Test", 0);

            Assert.Equal("Test", result.Title);
            _mockEventInteractor.Verify(m => m.Add(It.IsAny<Event?>()), Times.Once);
        }

        [Fact]
        public void CoreDeleteTest()
        {

            var result = _eventModule.AddEvent("Test", 0);
            _eventModule.DeleteEvent(result.Id);

            Assert.Equal(null, _eventModule.GetEvent(result.Id));
            _mockEventInteractor.Verify(m => m.Add(It.IsAny<Event?>()), Times.Once);
            _mockEventInteractor.Verify(m => m.Delete(It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public void CoreUpdateTest()
        {

            var result = _eventModule.AddEvent("Test", 0);
            result = _eventModule.UpdateEvent(result.Id, "NewTest");

            Assert.Equal("NewTest", result.Title);
            _mockEventInteractor.Verify(m => m.Add(It.IsAny<Event?>()), Times.Once);
            _mockEventInteractor.Verify(m => m.Update(It.IsAny<long>(), It.IsAny<Event?>()), Times.Once);
        }

        [Fact]
        public void CoreGetTest()
        {

            var result = _eventModule.AddEvent("Test", 0);
            result = _eventModule.GetEvent(result.Id);


            Assert.Equal("Test", result.Title);
            _mockEventInteractor.Verify(m => m.Add(It.IsAny<Event?>()), Times.Once);
            _mockEventInteractor.Verify(m => m.Get(It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public void EdgeAddTest()
        {

            var result = _eventModule.AddEvent(null, 0);


            Assert.Equal(null, result.Title);
            _mockEventInteractor.Verify(m => m.Add(It.IsAny<Event?>()), Times.Once);
        }

        [Fact]
        public void EdgeUpdateTest()
        {
            var result = _eventModule.AddEvent("Test", 0);
            result = _eventModule.UpdateEvent(result.Id, null);

            Assert.Equal(null, result.Title);
            _mockEventInteractor.Verify(m => m.Add(It.IsAny<Event?>()), Times.Once);
            _mockEventInteractor.Verify(m => m.Update(It.IsAny<long>(), It.IsAny<Event?>()), Times.Once);
        }

        [Fact]
        public void EdgeGetTest()
        {
            var result = _eventModule.AddEvent(null, 0);
            result = _eventModule.GetEvent(result.Id);


            Assert.Equal(null, result.Title);
            _mockEventInteractor.Verify(m => m.Add(It.IsAny<Event?>()), Times.Once);
            _mockEventInteractor.Verify(m => m.Get(It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public void ExeptUpdateTest()
        {
            var result_old = _eventModule.AddEvent("Test", 0);
            var result_new = _eventModule.UpdateEvent(-1, "NewTest");
            result_old = _eventModule.GetEvent(result_old.Id);

            Assert.Equal("Test", result_old.Title);
            Assert.Equal(null, result_new);
            _mockEventInteractor.Verify(m => m.Add(It.IsAny<Event?>()), Times.Once);
            _mockEventInteractor.Verify(m => m.Update(It.IsAny<long>(), It.IsAny<Event?>()), Times.Never);
            _mockEventInteractor.Verify(m => m.Get(It.IsAny<long>()), Times.AtLeastOnce);
        }

        [Fact]
        public void ExeptDeleteTest()
        {

            _eventModule.DeleteEvent(-1);
            _mockEventInteractor.Verify(m => m.Delete(It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public void ExeptGetTest()
        {
            var result = _eventModule.GetEvent(0);

            Assert.Equal(null, result);
            _mockEventInteractor.Verify(m => m.Get(It.IsAny<long>()), Times.Once);
        }
    }
}
