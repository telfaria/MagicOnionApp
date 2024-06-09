using System;
using System.Diagnostics;
using Grpc.Core;
using MagicOnion;
using MagicOnion.Client;
using MagicOnion.Server;
using MagicOnion.Server.Hubs;
using MessagePack;
using MagicOnionApp.Shared.MessagePackObjects;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace MagicOnionApp.Shared
{
    public interface IChatHub : IStreamingHub<IChatHub, IChatHubReceiver>
    {
        Task JoinAsync(string userName);
        Task LeaveAsync();
        Task SendMessageAsync(string message);
    }

    public interface IChatHubReceiver
    {
        void OnReceiveMessage(string userName, string message);
    }

    public class ChatHub : StreamingHubBase<IChatHub, IChatHubReceiver>, IChatHub
    {
        IGroup room;
        string userName;

        public async Task JoinAsync(string userName)
        {
            this.userName = userName;
            this.room = await Group.AddAsync("DefaultRoom");
            //await this.room.AddAsync(this.Context);
            BroadcastExceptSelf(room).OnReceiveMessage("Welcome",this.userName);
            Console.WriteLine($"Join {userName} from {room}");
        }

        public async Task LeaveAsync()
        {
            await this.room.RemoveAsync(this.Context);
            Console.WriteLine($"Leave {userName}");
        }

        public async Task SendMessageAsync(string message)
        {
            Console.WriteLine($"ReceiveMessage:  {userName} : {message}");
            BroadcastExceptSelf(room).OnReceiveMessage(userName, message);
            Console.WriteLine($"SendMessage: {userName} : {message}");
        }
    }
}