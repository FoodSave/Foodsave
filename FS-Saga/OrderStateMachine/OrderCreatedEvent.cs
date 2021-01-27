using System;
using System.Collections.Generic;
using FS_Saga.OrderStateMachine;
using MessagingContracts.Orders;

namespace FS_Saga.StateMachines
{
    public class OrderCreatedEvent : IOrderCreatedEvent
    {
        private readonly OrderStateData _orderSagaState;
        
        public OrderCreatedEvent(OrderStateData orderStateData)
        {
            this._orderSagaState = orderStateData;
        }

        public Guid OrderId => _orderSagaState.OrderId;
        public List<PackageId> PackagesIds => _orderSagaState.PackagesId;
    }
}