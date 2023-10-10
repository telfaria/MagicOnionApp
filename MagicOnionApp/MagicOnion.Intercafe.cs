using System;
using MagicOnion;

namespace MagicOnionApp.Shared
{
    public interface IMagicOnionAppService : IService<IMagicOnionAppService>
    {
        Task<int> SumAsync(int x, int y);
    }

}

