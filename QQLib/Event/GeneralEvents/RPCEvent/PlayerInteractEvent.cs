using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eco.Shared.Networking;
using Eco.Shared.Serialization;

namespace QQLib.Event.GeneralEvents.RPCEvent
{
    public class PlayerInteract : RPCEvent
    {
        public PlayerInteract(INetClient client, NetworkEvent netEvent, BSONObject bson, string methodName) : base(client, netEvent, bson, methodName)
        {

        }
    }
}
