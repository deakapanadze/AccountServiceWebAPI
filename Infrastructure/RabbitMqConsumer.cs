using Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using IModel = RabbitMQ.Client.IModel;



namespace Infrastructure
{
    public class RabbitMqConsumer
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IAccountRepository _accountRepository;

        public RabbitMqConsumer(IAccountRepository accountRepository)
        {

            _accountRepository = accountRepository;
            var factory = new ConnectionFactory() { HostName = "127.0.0.1:15672" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "transfer_queue",
                                   durable: false,
                                   exclusive: false,
                                   autoDelete: false,
                                   arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var transfer = JsonConverter.DeserializeObject<Transfer>(message);


                await ProccessTransferAsync(transfer);
            };
            
            _channel.BasicConsume(queue:"transfer_queue",
                                    autoAck: true,
                                     consumer: consumer);



        }




        private async Task ProccessTransferAsync (Transfer transfer)
        {
            var SourceAccount = await _accountRepository.GetAccountByNumberAsync(transfer.SourceAccountNumber);
            var destionatioAccount = await _accountRepository.GetAccountByNumberAsync(transfer.DestinationAccountNumber);
            if (SourceAccount != null && destionatioAccount != null)
            {


                SourceAccount.Balance -= transfer.Amount;
                destionatioAccount.Balance += transfer.Amount;

                await _accountRepository.UpdateAccountAsync(SourceAccount);
                await _accountRepository.UpdateAccountAsync(destionatioAccount);
            }
        }
       
    }
}
