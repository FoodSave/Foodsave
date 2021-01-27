using System;
using System.Collections.Generic;
using FS_Saga.StateMachines;
using MessagingContracts.Orders;

namespace FS_Saga.OrderStateMachine.Events
{
    public class OrderCollectedEvent : IOrderCollectedEvent
    {
        private readonly OrderStateData _orderSagaState;
        
        public OrderCollectedEvent(OrderStateData orderStateData)
        {
            this._orderSagaState = orderStateData;
        }

        public Guid OrderId => _orderSagaState.OrderId;
        public List<PackageId> PackagesIds => _orderSagaState.PackagesId;
    }
}