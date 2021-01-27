using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using MessagingContracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using PaymentService;


namespace OrderValidationService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMassTransit(config =>
            {
                config.AddConsumer<OrderValidationConsumer>();
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(EnvironmentVariables.RabbitMqConnectionString);

                    cfg.ReceiveEndpoint("Validate-package-queue", c =>
                    {
                        c.ConfigureConsumer<OrderValidationConsumer>(ctx);

                    });
                   
                });
            });

            services.AddMassTransitHostedService();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
        }
    }
}