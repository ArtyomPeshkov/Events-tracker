﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IEventModule
    {
        Event AddEvent(string eventName, long userId);
        Event UpdateEvent(long eventId, string newEventName);
        void DeleteEvent(long eventId);
        Event GetEvent(long eventId);
    }

}
