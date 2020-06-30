using IMSRepository.BusService.Interface;
using Microsoft.Azure.ServiceBus;
using System.Text;

namespace IMSRepository.BusService
{

    public class BusMessageService: IBusMessageService
    {
        private readonly ITopicClient _topicClient;

        public BusMessageService(ITopicClient topicClient)
        {
            _topicClient = topicClient;
        }
        
        public async void SendMessage<T>(T type)
        {

            var message = new Message(Encoding.UTF8.GetBytes(System.Text.Json.JsonSerializer.Serialize<T>(type)));

            await _topicClient.SendAsync(message);
        }

    }
}
