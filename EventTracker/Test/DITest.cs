using Moq;
using Domain;
using UI;
using Microsoft.Extensions.DependencyInjection;
using Autofac;

namespace Tests
{
    public class EventInteractorMock : IEventDatabaseInteractor
    {
        private Dictionary<long, Event> _events = new Dictionary<long, Event>();
        private static long _nextId = 1;

        public Event Add(Event eventItem)
        {
            eventItem.Id = _nextId++;
            _events[eventItem.Id] = eventItem;
            return _events[eventItem.Id];
        }

        public void Delete(long eventId)
        {
            _events.Remove(eventId);
        }

        public Event Get(long eventId)
        {
            _events.TryGetValue(eventId, out var eventItem);
            return eventItem;
        }

        public Event Update(long eventId, Event eventItem)
        {
            if (!_events.ContainsKey(eventId))
                throw new KeyNotFoundException("Event not found.");

            eventItem.Id = eventId;
            _events[eventId] = eventItem;
            return _events[eventId];
        }
    }

    public class MicrosoftDITests
    {
        private readonly Mock<IEventDatabaseInteractor> _mockEventInteractor;
        private readonly EventModule _eventModule;

        public MicrosoftDITests()
        {
            var container = DISetup.GetContainer(typeof(EventInteractorMock));
            _eventModule = new EventModule(container.Resolve<IEventDatabaseInteractor>());
        }

        [Fact]
        public void CoreAddTest()
        {
            var result = _eventModule.AddEvent("Test", "Test", 0);
            Assert.Equal("Test", result.Title);
            Assert.True(result.Id > 0);
        }

        [Fact]
        public void CoreDeleteTest()
        {
            var result = _eventModule.AddEvent("Test", "Test", 0);
            _eventModule.DeleteEvent(result.Id);
            Assert.Null(_eventModule.GetEvent(result.Id));
        }

        [Fact]
        public void CoreUpdateTest()
        {
            var result = _eventModule.AddEvent("Test", "Test", 0);
            result = _eventModule.UpdateEvent(result.Id, "NewTest", "NewTest");
            Assert.Equal("NewTest", result.Title);
        }

        [Fact]
        public void CoreGetTest()
        {
            var result = _eventModule.AddEvent("Test", "Test", 0);
            result = _eventModule.GetEvent(result.Id);
            Assert.Equal("Test", result.Title);
        }

        [Fact]
        public void CoreDeleteNonExistentEventTest()
        {
            var result = _eventModule.AddEvent("Test", "Test", 0);
            _eventModule.DeleteEvent(result.Id);
            _eventModule.DeleteEvent(result.Id); 
            Assert.Null(_eventModule.GetEvent(result.Id));
        }

        [Fact]
        public void CoreAddMultipleEventsTest()
        {
            var event1 = _eventModule.AddEvent("Event 1", "Event 1", 0);
            var event2 = _eventModule.AddEvent("Event 2", "Event 2", 0);

            Assert.NotEqual(event1.Id, event2.Id);
            Assert.Equal("Event 1", event1.Title);
            Assert.Equal("Event 2", event2.Title);
        }

        [Fact]
        public void CoreGetAllEventsTest()
        {
            var event1 = _eventModule.AddEvent("Event 1", "Event 1", 0);
            var event2 = _eventModule.AddEvent("Event 2", "Event 2", 0);

            Assert.Equal(event1, _eventModule.GetEvent(event1.Id));
            Assert.Equal(event2, _eventModule.GetEvent(event2.Id));
        }

    }
}
