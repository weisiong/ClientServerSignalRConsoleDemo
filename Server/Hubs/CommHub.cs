using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Server.Hubs
{
    public class CommHub : Hub
    {
        public async Task SendToAllAsync(string message)
        {
            //Client ReceiveMessage
            await Clients.All.SendAsync("ReceiveMessage", AppendTimeStamp(message));
        }

        private string AppendTimeStamp(string message)
        {
            return $"{message} : {DateTimeOffset.Now.ToString("yyyy/MM/dd/HH:mm:ss.fff")}";
        }

    }

}
