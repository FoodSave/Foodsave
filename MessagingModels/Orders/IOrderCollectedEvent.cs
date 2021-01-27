using System;
using System.Collections.Generic;

namespace MessagingContracts.Orders
{
    public interface IOrderCollectedEvent
    {
        public Guid OrderId { get;  }
        public List<PackageId> PackagesIds { get;  }
        
    }
}