using MagicOnion;
using MagicOnion.Client;
using MessagePack;
using System.Threading.Channels;
using Grpc.Core;
using Grpc.Net.Client;
using MagicOnionApp.Shared;

namespace MagicOnionApp.Client
{
    public partial class Form1 : Form
    {
        private GrpcChannel channel;
        private IMagicOnionAppService client;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            connect();
        }

        private void connect()
        {
            channel = GrpcChannel.ForAddress("http://localhost:5000");
            client = MagicOnionClient.Create<IMagicOnionAppService>(channel);

            txtResult.AppendText("Connect: http://localhost:5000 \r\n");

        }

        private async void btnQuery_Click(object sender, EventArgs e)
        {
            int x = new Random().Next();
            int y = new Random().Next();

            var result = await client.SumAsync(x, y);

            txtResult.AppendText($"{x} + {y} = {result.ToString()}\r\n");
        }
    }
}