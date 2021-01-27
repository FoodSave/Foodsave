using System;
using System.Collections.Generic;

namespace MessagingContracts.Orders
{
    public interface IOrderPayedEvent
    {
        public Guid OrderId { get;  }
        public List<PackageId> PackagesIds { get;  }
        public string PaymentMethod { get;  }
    }
}