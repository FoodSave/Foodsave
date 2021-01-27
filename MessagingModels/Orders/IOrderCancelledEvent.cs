using System;
using System.Collections.Generic;

namespace MessagingContracts.Orders
{
    public interface IOrderCancelledEvent
    {
        public Guid OrderId { get; set; }
        public List<PackageId> PackagesIds { get; set; }
    }
}