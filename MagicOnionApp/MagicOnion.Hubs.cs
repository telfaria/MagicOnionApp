using System;
using MagicOnion;
using MagicOnion.Server;
using MagicOnion.Server.Hubs;
using MessagePack;

namespace MagicOnionApp.Hubs
{

    public interface IGroupHub : IStreamingHub<IGroupHub, IGroupHubReceiver>
    {
        Task JoinAsync(string roomname, string name);

        Task LeaveAsync();

        Task SendMessageAsync(string username, string message);
    }

    public interface IGroupHubReceiver
    {
        void OnJoin(string name);

        void OnLeave(string name);

        void OnSendMessage(string name, string message);
    }


    public class GroupHubReceiver : StreamingHubBase<IGroupHub,IGroupHubReceiver> , IGroupHub
    {
        private IGroup group;
        private string username;

        public async Task JoinAsync(string room, string name)
        {
            this.username = name;
            group = await Group.AddAsync(room);
            Broadcast(group).OnJoin(name);
        }

        public async Task LeaveAsync()
        {
            await group.RemoveAsync(this.Context);
            Broadcast(group).OnLeave(username);
        }

        public async Task SendMessageAsync(string name, string message)
        {
            Broadcast(group).OnSendMessage(name, message);
            await Task.CompletedTask;
        }

    }

    public class Grouphub : IGroupHubReceiver
    {
        private string roomname = "RoomC";
        private string username = "user1";

        public async void OnJoin(string name)
        {
            //return $"Connected {name}";
        }

        public async void OnLeave(string name)
        {
            //return $"Disconnected {name}";
        }

        public async void OnSendMessage(string name, string message)
        {
            //return $"{name} : {message}";
        }
    }

}