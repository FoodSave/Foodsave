using System;

namespace PaymentService
{
    public static class EnvironmentVariables
    {
        public static string RabbitMqConnectionString { get; } = Environment.GetEnvironmentVariable("RabbitMQConnectionString");
    }
}