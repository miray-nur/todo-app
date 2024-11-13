using Business.Abstract;
using Business.Configurations;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Xml.Serialization;

namespace Business
{
    public class QueueService : IQueueService
    {
        private readonly IEmailService _emailService;
        private readonly RabbitMqSettings _rabbitMqSettings;
        public QueueService(IEmailService emailService, IOptions<RabbitMqSettings> rabbitMqSettings)
        {
            _emailService = emailService;
            _rabbitMqSettings = rabbitMqSettings.Value;
        }
        public void Publisher(EmailModel emailModel)
        {
            var factory = new ConnectionFactory { HostName = _rabbitMqSettings.Connection, UserName = "user", Password = "pass" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("emailQueue", true, false, false);

            var xmlSerializer = new XmlSerializer(typeof(EmailModel));
            using var stringWriter = new StringWriter();
            xmlSerializer.Serialize(stringWriter, emailModel);
            var body = Encoding.UTF8.GetBytes(stringWriter.ToString());
            channel.BasicPublish(exchange: "", routingKey: "emailQueue", body: body);
        }

        public async Task Consumer()
        {
            var factory = new ConnectionFactory { HostName = _rabbitMqSettings.Connection, UserName = "user", Password = "pass" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare("emailQueue", true, false, false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var xmlSerializer = new XmlSerializer(typeof(EmailModel));
                using var stringReader = new StringReader(message);
                var emailModel = (EmailModel)xmlSerializer.Deserialize(stringReader);

                _emailService.SendMail(emailModel);
            };

            channel.BasicConsume("emailQueue", true, consumer);
            await Task.Delay(-1);
        }
    }


    //{
    //    private readonly Queue<EmailModel> _mailQueue = new Queue<EmailModel>();

    //    private IEmailService _emailService;
    //    public QueueService(IEmailService emailService)
    //    {
    //        _emailService = emailService;
    //    }
    //    public void EnqueueMail(EmailModel emailModel)
    //    {
    //        _mailQueue.Enqueue(emailModel);
    //    }

    //    public void ProcessQueue()
    //    {
    //        Task.Run(async () =>
    //        {
    //            while (true)
    //            {
    //                if (_mailQueue.Count > 0)
    //                {
    //                    EmailModel emailModel = _mailQueue.Dequeue();
    //                    _emailService.SendMail(emailModel);
    //                }
    //                await Task.Delay(10000);
    //            }

    //        });
    //    }
    //}
}
