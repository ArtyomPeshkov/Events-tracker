using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IEventModule
    {
        Event AddEvent(string eventName, string description, long userId);
        Event UpdateEvent(long eventId, string newEventName, string description);
        void DeleteEvent(long eventId);
        Event GetEvent(long eventId);
    }

}
