using MagicOnion.Server;
using MagicOnionApp.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MagicOnionApp.Server
{

    public class MagicOnionAppService : ServiceBase<IMagicOnionAppService>, IMagicOnionAppService
    {
        public async Task<int> SumAsync(int x, int y)
        {
            Console.WriteLine("Called SumAsync");
            return x + y;
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddGrpc();
            builder.Services.AddMagicOnion();

            var app=builder.Build();

            app.MapMagicOnionService();

            app.Run();
        }
    }
}