using System;
using System.Collections.Generic;

namespace MessagingContracts.Orders
{
    public interface IOrderStartedEvent
    {
        public Guid OrderId { get;  }
        public List<PackageId> PackagesIds { get;  }
    }
}