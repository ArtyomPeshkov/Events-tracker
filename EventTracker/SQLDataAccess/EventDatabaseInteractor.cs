using Domain;

namespace SQLDataAccess
{
    public class EventDatabaseInteractor : IEventDatabaseInteractor
    {
        private readonly IDatabase _database;

        public EventDatabaseInteractor(IDatabase database)
        {
            _database = database;
        }

        public Event Add(Event eventItem)
        {
            // TODO: add support of passing categories ids
            return _database.AddEvent(eventItem);
        }

        public Event Update(long id, Event eventItem)
        {
            // TODO: add support of passing categories ids
            return _database.UpdateEvent(id, eventItem);
        }

        public void Delete(long eventId)
        {
            _database.DeleteEvent(eventId);
        }

        public Event Get(long eventId)
        {
            return _database.GetEvent(eventId);
        }
    }
}
