using Business.Abstract;
using Business.Configurations;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Business.Concrete
{
    public class EmailService : IEmailService
    {
        #region Members
        private readonly MailSettings _mailSettings;
        #endregion

        #region Constructor

        public EmailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        #endregion

        #region Methods

        public void SendMail(EmailModel emailModel)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Company", _mailSettings.FromMail));
            message.To.Add(new MailboxAddress("User", emailModel.ToMail));
            message.Subject = emailModel.Subject;
            message.Body = new TextPart("plain") { Text = emailModel.Body };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate(_mailSettings.FromMail, _mailSettings.Password);
                client.Send(message);
                client.Disconnect(true);
            }

            //Console.WriteLine($"Email sent {_mailSettings.FromMail} to {emailModel.ToMail}, Subject: {emailModel.Subject}, Content: {emailModel.Body}");
        }

        #endregion
    }
}
