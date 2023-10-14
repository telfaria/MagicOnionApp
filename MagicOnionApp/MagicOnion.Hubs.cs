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

namespace MagicOnionApp.Shared
{

    public interface IGroupHub : IStreamingHub<IGroupHub, IGroupHubReceiver>
    {
        ValueTask<BroadCastMessages[]> JoinAsync(string room, string name);

        ValueTask LeaveAsync();

        Task<BroadCastMessages> SendMessageAsync(string username, string message);
    }

    public interface IGroupHubReceiver
    {
        void OnJoin(BroadCastMessages bcm);

        void OnLeave(BroadCastMessages bcm);

        void OnSendMessage(BroadCastMessages bcm);
    }


    public class GroupHub : StreamingHubBase<IGroupHub,IGroupHubReceiver> , IGroupHub
    {
        private IGroup group;
        private BroadCastMessages bcm;
        private IInMemoryStorage<BroadCastMessages> storage;

        public async ValueTask<BroadCastMessages[]> JoinAsync(string room, string name)
        {
            bcm = new BroadCastMessages();
            bcm.message = $"Join {name} : Room {room} \r\n";
            bcm.username = name;

            (group,storage) = await Group.AddAsync(room,bcm);

            Broadcast(group).OnJoin(bcm);
            return storage.AllValues.ToArray();
        }

        public async ValueTask LeaveAsync()
        {
            await group.RemoveAsync(this.Context);
            Broadcast(group).OnLeave(bcm);
        }

        public async Task<BroadCastMessages> SendMessageAsync(string name, string message)
        {
            bcm = new BroadCastMessages() { message = message, username = name };
            Broadcast(group).OnSendMessage(bcm);
            await Task.CompletedTask;
            return bcm;
        }

    }

    public class GrouphubClient : IGroupHubReceiver
    {
        public Dictionary<string, BroadCastMessages> connectUsers = new Dictionary<string, BroadCastMessages>();

        private IGroupHub client;

        public async ValueTask<BroadCastMessages> ConnectAsync(ChannelBase grpcChannnel, string roomname,
            string username)
        {
            this.client = await StreamingHubClient.ConnectAsync<IGroupHub, IGroupHubReceiver>(grpcChannnel, this);

            var roomusers = await client.JoinAsync(roomname,username);

            foreach (var user in roomusers)
            {
                (this as IGroupHubReceiver).OnJoin(user);
            }
            return connectUsers[username];
        }

        public ValueTask LeaveAsync()
        {
            return client.LeaveAsync();
        }


        public async void OnJoin(BroadCastMessages bcm)
        {
            connectUsers[bcm.username] = bcm;
            //connectUsers[bcm.username].message = $"Welcome, {bcm.username}!";
            Debug.WriteLine(connectUsers[bcm.username].message);

        }

        public async void OnLeave(BroadCastMessages bcm)
        {
            connectUsers[bcm.username].message = $"Bye! {bcm.username}!";
            Debug.WriteLine(connectUsers[bcm.username].message);
        }

        public async void OnSendMessage(BroadCastMessages bcm)
        {
            connectUsers[bcm.username].message = bcm.message;
            Debug.WriteLine(connectUsers[bcm.username].message);
        }


    }

}