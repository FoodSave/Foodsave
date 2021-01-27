using System;
using System.Collections.Generic;
using FS_Saga.StateMachines;
using MessagingContracts.Orders;

namespace FS_Saga.OrderStateMachine.Events
{
    public class OrderPayedEvent : IOrderPayedEvent
    {
        private readonly OrderStateData _orderSagaState;
        
        public OrderPayedEvent(OrderStateData orderStateData)
        {
            this._orderSagaState = orderStateData;
        }

        public Guid OrderId => _orderSagaState.OrderId;
        public string PaymentMethod => _orderSagaState.PaymentMethod;
        public List<PackageId> PackagesIds => _orderSagaState.PackagesId;
    }
}