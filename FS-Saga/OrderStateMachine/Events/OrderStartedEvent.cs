using System;
using System.Collections.Generic;
using MessagingContracts.Orders;

namespace FS_Saga.OrderStateMachine.Events
{
    public class OrderStartedEvent : IOrderStartedEvent
    {
        private readonly OrderStateData _orderSagaState;
        
        public OrderStartedEvent(OrderStateData orderStateData)
        {
            this._orderSagaState = orderStateData;
        }

        public Guid OrderId => _orderSagaState.OrderId;
        public List<PackageId> PackagesIds => _orderSagaState.PackagesId;
    }
}