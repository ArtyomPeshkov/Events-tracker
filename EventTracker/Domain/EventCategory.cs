using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class EventCategory
    {
        public long EventId { get; set; } // Foreign key
        public Event Event { get; set; } // Navigation property

        public long CategoryId { get; set; } // Foreign key
        public Category Category { get; set; } // Navigation property
    }
}
