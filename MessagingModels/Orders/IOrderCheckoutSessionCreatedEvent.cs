using System;
using System.Collections.Generic;

namespace MessagingContracts.Orders
{
    public interface IOrderCheckoutSessionCreatedEvent
    {
        public Guid OrderId { get;  }
        public List<PackageId> PackagesIds { get;  }
        public string SessionId { get; }
    }
}