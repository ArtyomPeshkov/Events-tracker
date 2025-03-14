using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public static class Interactors
    {
        public static IUserDatabaseInteractor UserInteractor { get; set; }
        public static ICategoryDatabaseInteractor CategoryInteractor { get; set; }
        public static IEventDatabaseInteractor EventInteractor { get; set; }
    }
}
