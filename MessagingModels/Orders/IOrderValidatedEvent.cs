using System;
using System.Collections.Generic;

namespace MessagingContracts.Orders
{
    public interface IOrderValidatedEvent
    {
        public Guid OrderId { get;  }
        public List<PackageId> PackagesIds { get;  }
    }
}