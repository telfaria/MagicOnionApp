using MagicOnion;
using MagicOnion.Client;
using MessagePack;
using System.Threading.Channels;
using Grpc.Core;
using Grpc.Net.Client;
using MagicOnion.Server.Hubs;
using MagicOnionApp.Shared;
using MagicOnionApp.Shared.MessagePackObjects;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Windows.Forms;

namespace MagicOnionApp.Client
{
    public partial class Form1 : Form, IChatHubReceiver
    {
        private GrpcChannel channel;
        private IMagicOnionAppService client;
        //private CancellationTokenSource shutdowncancelletion = new CancellationTokenSource();
        //private IGroupHub streamingclient;
        //private IGroupHubReceiver streamingclientreceiver;
        private BroadCastMessages broadcastmessage = new BroadCastMessages();
        private string currentUser;
        //private GrouphubClient hubClient = new GrouphubClient();
        private IChatHub chatHub;


        public Form1()
        {
            InitializeComponent();
            lblUserName.Text = $"StreamingHub Disconnected";
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
            currentUser = "User" + new Random().Next(1, 100);
            chatHub = StreamingHubClient.Connect<IChatHub, IChatHubReceiver>(channel, this);

            await chatHub.JoinAsync(currentUser);

            txtResult.AppendText($"Join {currentUser}{Environment.NewLine}");
            lblUserName.Text = $"Your:{currentUser}";

            //await Task.Delay(-1);
        }

        private async ValueTask DisConnectStreamingHub()
        {
            await chatHub.LeaveAsync();
            txtResult.AppendText($"Leave {currentUser}{Environment.NewLine}");
            await chatHub.DisposeAsync();
            lblUserName.Text = $"StreamingHub Disconnected";
        }

        public async void OnReceiveMessage(string username, string message)
        {
            //if (username == currentUser) return;
            txtResult.AppendText($"Receive: {username}: {message}{Environment.NewLine}");
        }



        private async void btnQuery_Click(object sender, EventArgs e)
        {
            int x = new Random().Next(1, 100);
            int y = new Random().Next(1, 100);

            var result = await client.SumAsync(x, y);

            txtResult.AppendText($"{x} + {y} = {result.ToString()}{Environment.NewLine}");
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
            txtResult.AppendText($"Send: {broadcastmessage.username}: {broadcastmessage.message}{Environment.NewLine}");
            chatHub.SendMessageAsync(broadcastmessage.message);
            txtMessage.Text = "";


        }

        private async void btnHubDisConnect_Click(object sender, EventArgs e)
        {
            await DisConnectStreamingHub();
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            txtResult.Clear();
        }
    }
}