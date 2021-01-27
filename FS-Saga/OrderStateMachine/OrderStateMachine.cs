using System;
using Automatonymous;
using FS_Saga.OrderStateMachine.Events;
using MessagingContracts.Orders;

namespace FS_Saga.OrderStateMachine
{
    public class OrderStateMachine : MassTransitStateMachine<OrderStateData>
    {
        public Event<IOrderCreatedEvent> StartOrderProcessEvent { get; private set; }
     //   public Event<IOrderStartedEvent> OrderStartedEvent { get; private set; }

        public Event<IOrderValidatedEvent> OrderValidatedEvent { get; private set; }
        public Event<IOrderCheckoutSessionCreatedEvent> CheckoutSessionCreatedEvent { get; private set; }
        public Event<IOrderPayedEvent> OrderPayedEvent { get; private set; }
        public Event<IOrderCollectedEvent> OrderCollectedEvent { get; private set; }
        public Event<IOrderCancelledEvent> OrderCancelledEvent { get; private set; }

        public State Started { get; private set; }
        public State Validated { get; private set; }
        public State CheckoutSessionCreated { get; private set; }
        public State Payed { get; private set; }
        public State Collected { get; private set; }

        public State Cancelled { get; private set; }

        
        //Start => Validated => Session created => Payed => Collected 
        public OrderStateMachine()
        {
            InstanceState(s => s.CurrentState);
            
            Event(() => StartOrderProcessEvent, x => x.CorrelateById(m => m.Message.OrderId));
          //  Event(() => OrderStartedEvent, x => x.CorrelateById(m => m.Message.OrderId));

            Event(() => OrderValidatedEvent, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => CheckoutSessionCreatedEvent, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => OrderPayedEvent, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => OrderCollectedEvent, x => x.CorrelateById(m => m.Message.OrderId));

            Event(() => OrderCancelledEvent, x => x.CorrelateById(m => m.Message.OrderId));

            //Start
            Initially(
                When(StartOrderProcessEvent)
                    .Then(context =>
                    {
                        context.Instance.OrderId = context.Data.OrderId;
                        context.Instance.PackagesId = context.Data.PackagesIds;
                        Console.WriteLine("Start");
                    })
                    .TransitionTo(Started)
                    .Publish(context => new OrderStartedEvent(context.Instance))
            );
            
            //Started  => Validated
            During(Started, When(
                    OrderValidatedEvent).Then(context =>
                {
                    context.Instance.OrderId = context.Data.OrderId;
                    context.Instance.PackagesId = context.Data.PackagesIds;
                    Console.WriteLine("Started => Validated");
                })
                    .TransitionTo(Validated)
               // .Publish(context => new OrderCheckoutSessionCreatedEvent(context.Instance))

            );
            //Validated => payment session created
            During(Validated, When(
                    CheckoutSessionCreatedEvent).Then(context =>
                {
                    context.Instance.OrderId = context.Data.OrderId;
                    context.Instance.PackagesId = context.Data.PackagesIds;
                    context.Instance.CheckoutSessionId = context.Data.SessionId;
                    Console.WriteLine("Validated => payment created");
                    Console.WriteLine(context.Instance.CheckoutSessionId);
                })
                
               // .Publish(context => new OrderCheckoutSessionCreatedEvent(context.Instance))
                .TransitionTo(CheckoutSessionCreated)
            );
            
            //Session created => payed
            During(CheckoutSessionCreated, When(
                    OrderPayedEvent).Then(context =>
                {
                    context.Instance.OrderId = context.Data.OrderId;
                    context.Instance.PackagesId = context.Data.PackagesIds;
                    context.Instance.PaymentMethod = context.Data.PaymentMethod;
                    Console.WriteLine("Created => payed");
                })
              //  .Publish(context => new OrderPayedEvent(context.Instance))
                .TransitionTo(Payed)
            );
            
            //payed => Collected
            During(Payed, When(
                    OrderCollectedEvent).Then(context =>
                {
                    context.Instance.OrderId = context.Data.OrderId;
                    context.Instance.PackagesId = context.Data.PackagesIds;
                    context.Instance.OrderCollectedDateTime = DateTime.Now;
                    Console.WriteLine("payed => collected");
                })
              //  .Publish(context => new OrderCollectedEvent(context.Instance))
                .TransitionTo(Collected)
            );
           
            //Cancel order bij cancelled event
            DuringAny(
                When(OrderCancelledEvent)
                    .Then(context =>
                    {
                        context.Instance.OrderCancelDateTime = DateTime.Now;
                        context.Instance.OrderId = context.Data.OrderId;
                        Console.WriteLine("Cancelled!");
                    })
                    .Finalize());


            SetCompletedWhenFinalized();
        }
    }
}