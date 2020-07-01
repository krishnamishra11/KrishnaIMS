namespace IMSRepository.BusService.Interface
{
    public interface IBusMessageService
    {
        public void SendMessage<T>(T type);
    }
}
