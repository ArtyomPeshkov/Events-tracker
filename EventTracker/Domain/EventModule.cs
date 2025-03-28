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

        public EventModule(IEventDatabaseInteractor eventInteractor)
        {
            _eventInteractor = eventInteractor;
        }

        public Event AddEvent(string eventName, string eventDescr, long userId)
        {
            return _eventInteractor.Add(new Event { Title = eventName, Description = eventDescr, UserId = userId });
        }

        public Event UpdateEvent(long eventId, string newEventName, string description)
        {
            Event e = GetEvent(eventId);
            if (e == null)
            {
                return null;
            }
            e.Title = newEventName;
            e.Description = description;
            Event result = null;
            try
            {
                result = _eventInteractor.Update(eventId, e);
            }
            catch (Exception ex)
            {
                return null;
            }
            return result;
        }

        public void DeleteEvent(long eventId)
        {
            _eventInteractor.Delete(eventId);
        }


        public Event GetEvent(long eventId)
        {
            Event result = null;
            try
            {
                result = _eventInteractor.Get(eventId);
            }
            catch (Exception ex)
            {
                return null;
            }
            return result;
        }
    }

}
