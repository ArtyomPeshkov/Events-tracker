using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Event
    {
        public Event() { }
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DeletionDate { get; set; }
        public DateTime NextNotificationDate { get; set; }
        public TimeSpan ProlongPeriod { get; set; }
        public TimeSpan DateShift { get; set; }
        public bool IsRegular { get; set; }
        public bool IsExtendable { get; set; }
        public long UserId { get; set; }
        public long CategoryId { get; set; }
    }
}
