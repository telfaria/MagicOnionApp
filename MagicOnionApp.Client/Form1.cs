using MagicOnion;
using MagicOnion.Client;
using MessagePack;
using System.Threading.Channels;
using Grpc.Core;
using Grpc.Net.Client;
using MagicOnion.Server.Hubs;
using MagicOnionApp.Shared;
using MagicOnionApp.Shared.MessagePackObjects;

namespace MagicOnionApp.Client
{
    public partial class Form1 : Form
    {
        private GrpcChannel channel;
        private IMagicOnionAppService client;
        private CancellationTokenSource shutdowncancelletion = new CancellationTokenSource();
        private IGroupHub streamingclient;
        private IGroupHubReceiver streamingclientreceiver;
        private BroadCastMessages broadcastmessage = new BroadCastMessages();
        private string currentUser;
        private GrouphubClient hubClient = new GrouphubClient();

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
            channel = GrpcChannel.ForAddress("http://localhost:5555");
            client = MagicOnionClient.Create<IMagicOnionAppService>(channel);

            txtResult.AppendText("Connect: http://localhost:5555 \r\n");
            //サーバーからクライアントにブロードキャストしたい


        }

        private async void ConnectStreamingHub()
        {
            channel = GrpcChannel.ForAddress("http://localhost:5555");
            currentUser = "User1";
            var ret = await hubClient.ConnectAsync(channel, "RoomC", currentUser);

            txtResult.AppendText(ret.message);

        }

        private async ValueTask DisConnectStreamingHub()
        {
            hubClient.LeaveAsync();
        }


        private async void btnQuery_Click(object sender, EventArgs e)
        {
            int x = new Random().Next(1, 100);
            int y = new Random().Next(1, 100);

            var result = await client.SumAsync(x, y);

            txtResult.AppendText($"{x} + {y} = {result.ToString()}\r\n");
        }

        private void btnhubConnect_Click(object sender, EventArgs e)
        {
            ConnectStreamingHub();
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            string mes = txtMessage.Text;
            broadcastmessage.username = currentUser;
            broadcastmessage.message = mes;
            hubClient.OnSendMessage(broadcastmessage);
            txtMessage.Text = "";


        }

        private void btnHubDisConnect_Click(object sender, EventArgs e)
        {
            hubClient.LeaveAsync();
        }
    }
}