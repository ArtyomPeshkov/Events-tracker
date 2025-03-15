using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IEventDatabaseInteractor
    {
        Event Add(Event eventItem);
        Event Update(long eventId, Event eventItem);
        void Delete(long eventId);
        Event Get(long eventId);
    }
}
