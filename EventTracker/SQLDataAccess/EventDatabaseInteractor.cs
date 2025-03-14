using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDataAccess
{
    public class EventDatabaseInteractor : IEventDatabaseInteractor
    {
        private readonly IDatabase _database;

        public EventDatabaseInteractor(IDatabase database)
        {
            _database = database;
        }

        public Event Add(string eventName)
        {
            return _database.Add("Events", new Event { Title = eventName});
        }

        public Event Update(long id, Event eventItem)
        {
            return _database.Update("Events", id, eventItem);
        }

        public void Delete(long eventId)
        {
            _database.Delete<Event>("Events", eventId);
        }

        public Event Get(long eventId)
        {
            return _database.Get<Event>("Events", eventId);
        }
    }
}
