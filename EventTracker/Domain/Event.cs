using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class EventBase
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class Event : EventBase
    {
        public DateTime? DeletionDate { get; set; }
        public DateTime NextNotificationDate { get; set; }
        public TimeSpan ProlongPeriod { get; set; }
        public TimeSpan DateShift { get; set; }
        public bool IsRegular { get; set; }
        public bool IsExtendable { get; set; }
        public long UserId { get; set; } // Foreign key

        public User User { get; set; } // Navigation property for 1:N relationship
        public ICollection<EventCategory> EventCategories { get; set; } // Navigation property for N:N relationship
    }
}
