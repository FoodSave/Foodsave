using System;
using System.Collections.Generic;
using MessagingContracts.Orders;

namespace FS_Saga.OrderStateMachine.Events
{
    public class OrderValidatedEvent : IOrderValidatedEvent
    {
        private readonly OrderStateData _orderSagaState;
        
        public OrderValidatedEvent(OrderStateData orderStateData)
        {
            this._orderSagaState = orderStateData;
        }

        public Guid OrderId => _orderSagaState.OrderId;
        public List<PackageId> PackagesIds => _orderSagaState.PackagesId;
    }
}