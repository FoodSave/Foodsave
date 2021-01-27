using System;
using System.Collections.Generic;
using Automatonymous;
using MessagingContracts.Orders;

namespace FS_Saga.OrderStateMachine
{
    public class OrderStateData : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; }
        public DateTime? OrderCreationDateTime { get; set; }
        public DateTime? OrderCancelDateTime { get; set; }
        public DateTime? OrderCollectedDateTime { get; set; }

        public Guid OrderId { get; set; }
        public List<PackageId> PackagesId { get; set; }
        public string CheckoutSessionId { get; set; }
        public string PaymentMethod { get; set; }
    }
}