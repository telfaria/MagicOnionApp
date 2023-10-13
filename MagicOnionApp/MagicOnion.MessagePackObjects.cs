using System;
using MagicOnion;
using MagicOnion.Server;
using MessagePack;

namespace MagicOnionApp.Shared.MessagePackObjects
{
    [MessagePackObject]    
    public class BroadCastMessages
    {
        [Key(0)]
        public string message { get; set; }

    }

}
