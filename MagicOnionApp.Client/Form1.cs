using MagicOnion;
using MagicOnion.Client;
using MessagePack;
using System.Threading.Channels;
using Grpc.Core;
using Grpc.Net.Client;
using MagicOnion.Server.Hubs;
using MagicOnionApp.Hubs;
using MagicOnionApp.Shared;

namespace MagicOnionApp.Client
{
    public partial class Form1 : Form
    {
        private GrpcChannel channel;
        private IMagicOnionAppService client;
        private CancellationTokenSource shutdowncancelletion = new CancellationTokenSource();
        private IGroupHub streamingclient;
        private IGroupHubReceiver streamingclientreceiver;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            connect();
        }

        private async void connect()
        {
            channel = GrpcChannel.ForAddress("http://localhost:5000");
            client = MagicOnionClient.Create<IMagicOnionAppService>(channel);
            streamingclient =
                await StreamingHubClient.ConnectAsync<IGroupHub, IGroupHubReceiver>(channel, streamingclientreceiver,
                    cancellationToken: shutdowncancelletion.Token);
            
            txtResult.AppendText("Connect: http://localhost:5000 \r\n");
            var res = await streamingclient.JoinAsync("RoomC", "User1");

            

        }

        private async void btnQuery_Click(object sender, EventArgs e)
        {
            int x = new Random().Next(1,100);
            int y = new Random().Next(1,100);

            var result = await client.SumAsync(x, y);

            txtResult.AppendText($"{x} + {y} = {result.ToString()}\r\n");
        }




    }
}