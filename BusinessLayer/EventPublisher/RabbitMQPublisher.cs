using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace BusinessLayer.EventPublisher
{
    public class RabbitMQPublisher
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly RabbitMQ.Client.IModel _channel;
        private readonly string _exchange;
        private readonly string _routingKey;

        public RabbitMQPublisher(IConfiguration configuration)
        {
            _configuration = configuration;
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQ:Host"],
                UserName = _configuration["RabbitMQ:UserName"],
                Password = _configuration["RabbitMQ:Password"]
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _exchange = _configuration["RabbitMQ:Exchange"];
            _routingKey = _configuration["RabbitMQ:RoutingKey"];

            _channel.ExchangeDeclare(_exchange, ExchangeType.Direct);
        }

        public void PublishMessage(object message)
        {
            var jsonMessage = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(jsonMessage);

            _channel.BasicPublish(exchange: _exchange, routingKey: _routingKey, body: body);
        }
    }
}
