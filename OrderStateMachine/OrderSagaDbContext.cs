using System.Collections.Generic;
using MassTransit.EntityFrameworkCoreIntegration;
using MassTransit.EntityFrameworkCoreIntegration.Mappings;
using MessagingContracts.Orders;
using Microsoft.EntityFrameworkCore;

namespace OrderStateMachine
{
    public class OrderSagaDbContext : SagaDbContext
    {

        public OrderSagaDbContext(DbContextOptions<OrderSagaDbContext> options)
            : base(options)
        {

        }
        

        protected override IEnumerable<ISagaClassMap> Configurations
        {
            get { yield return new OrderStateMap(); }
        }
    }
}