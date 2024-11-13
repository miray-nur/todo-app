namespace Business.Abstract
{
    public interface IEmailService
    {
        void SendMail(EmailModel emailModel);
    }
}
