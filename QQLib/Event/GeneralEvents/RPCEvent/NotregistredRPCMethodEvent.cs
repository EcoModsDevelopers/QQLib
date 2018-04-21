using Eco.Shared.Networking;
using Eco.Shared.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQLib.Event.GeneralEvents.RPCEvent
{
    public class NotregistredRPCMethodEvent : RPCEvent
    {
        public NotregistredRPCMethodEvent(INetClient client, NetworkEvent netEvent, BSONObject bson, string methodName) : base(client, netEvent, bson, methodName)
        {

        }
    }
}
