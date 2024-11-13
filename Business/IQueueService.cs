namespace Business
{
    public interface IQueueService
    {
        void Publisher(EmailModel emailModel);
        Task Consumer();
    }
}
