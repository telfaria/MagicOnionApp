using System;
using MagicOnion;
using MagicOnion.Server;
using MessagePack;

namespace MagicOnionApp.Shared
{
    public interface IMagicOnionAppService : IService<IMagicOnionAppService>
    {
        UnaryResult<int> SumAsync(int x, int y);
    }
    public class MagicOnionAppService : ServiceBase<IMagicOnionAppService>, IMagicOnionAppService
    {

        public async UnaryResult<int> SumAsync(int x, int y)
        {
            Console.WriteLine("Called SumAsync");
            return x + y;
        }
    }

}

