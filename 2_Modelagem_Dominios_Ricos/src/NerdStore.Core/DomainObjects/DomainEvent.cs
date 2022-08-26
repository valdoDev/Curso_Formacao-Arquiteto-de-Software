﻿using NerdStore.Core.Messages;
using System;

namespace NerdStore.Core.DomainObjects
{
    public abstract class DomainEvent : Event
    {
        public DateTime Timestamp { get; private set; }

        protected DomainEvent(Guid aggregateId)
        {
            AggregateId = aggregateId;
            Timestamp = DateTime.Now;
        }
    }
}
