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
        public string username { get; set; }
        
        [Key(1)]
        public string message { get; set; }


    }

}
