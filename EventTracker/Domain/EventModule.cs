using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class EventModule : IEventModule
    {
        private readonly IEventDatabaseInteractor _eventInteractor;

        public EventModule()
        {
            _eventInteractor = Interactors.EventInteractor;
        }

        public Event AddEvent(string eventName)
        {
            var result = _eventInteractor.Add(eventName);
            return result;
        }

        public void UpdateEvent(long eventId, string newEventName)
        {
            Event e = new Event();
            e.Title = newEventName;
            _eventInteractor.Update(eventId, e);
        }

        public void DeleteEvent(long eventId)
        {
            _eventInteractor.Delete(eventId);
        }


        public Event GetEvent(long eventId)
        {
            return _eventInteractor.Get(eventId);
        }
    }

}
