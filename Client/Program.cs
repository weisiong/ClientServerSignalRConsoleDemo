using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            HubConnection connection;

            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:44304/CommHub")
                .WithAutomaticReconnect()
                .Build();

            connection.On("ReceiveMessage", (string messageFromServer) =>
            {
                Console.WriteLine($"Rx from server:\n {messageFromServer}");
            });

            try
            {
                await connection.StartAsync();
                Console.WriteLine("Start Connection");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection Failed:{ex.Message}");
            }

            while (true)
            {
                Console.Write("Message To Server: ");

                string input = Console.ReadLine();

                try
                {
                    //メッセージ送信
                    await connection.InvokeAsync<string>("SendToAllAsync", input);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Send Message Failed:{ex.Message}");
                }

                Thread.Sleep(500);
            }
        }

        //private static void ReceiveMessageClient(string messageFromServer)
        //{
        //    Console.WriteLine($"From Svr {DateTimeOffset.Now.ToString("yyyy/MM/dd/HH:mm:ss.fff")} message:\n {messageFromServer}");
        //}

    }
}
