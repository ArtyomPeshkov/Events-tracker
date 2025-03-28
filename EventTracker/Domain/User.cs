using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class User
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }

        public Profile Profile { get; set; } // 1:1 relationship
        public ICollection<Event> Events { get; set; } // 1:N relationship
    }
}
