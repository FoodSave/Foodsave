using System;
using System.Collections.Generic;
using FS_Saga.OrderStateMachine;
using MessagingContracts.Orders;

namespace FS_Saga.OrderStateMachine.Events
{
    public class OrderCheckoutSessionCreatedEvent : IOrderCheckoutSessionCreatedEvent
    {
        private readonly OrderStateData _orderSagaState;
        
        public OrderCheckoutSessionCreatedEvent(OrderStateData orderStateData)
        {
            this._orderSagaState = orderStateData;
        }

        public Guid OrderId => _orderSagaState.OrderId;
        public List<PackageId> PackagesIds => _orderSagaState.PackagesId;
        public string SessionId => _orderSagaState.CheckoutSessionId;
    }
}