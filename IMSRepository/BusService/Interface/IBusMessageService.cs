using System;
using System.Collections.Generic;
using System.Text;

namespace IMSRepository.BusService.Interface
{
    public interface IBusMessageService
    {
        public void SendMessage<T>(T type);
    }
}
