using MassTransit;
using MassTransit.RabbitMqTransport;
using System;
using System.Threading.Tasks;
using FS_Saga.OrderStateMachine;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace OrderStateMachine
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var connString = "server=rollerbankapp.nl;database=FoodSaveOrderState;user=foodsave;password=Eenden123!";
      
            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(cfg =>
                    {
                        cfg.AddSagaStateMachine<FS_Saga.OrderStateMachine.OrderStateMachine, OrderStateData>()
                            
                            .EntityFrameworkRepository(r =>
                            {
                                r.ConcurrencyMode =
                                    ConcurrencyMode.Pessimistic; // or use Optimistic, which requires RowVersion

                                r.AddDbContext<DbContext, OrderSagaDbContext>((provider, builder) =>
                                {
                                    builder.UseMySql(connString, ServerVersion.AutoDetect(connString));
                                });
                            });
                       cfg.AddBus(b=> ConfigureBus(b));
                    });

                    services.AddMassTransitHostedService();
                });

            await builder.RunConsoleAsync();
        }
        public static IBusControl ConfigureBus(IServiceProvider provider, Action<IRabbitMqBusFactoryConfigurator, IRabbitMqHost>
            registrationAction = null)
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("username");
                    h.Password("password");
                });

             
            });
        }
    }
}