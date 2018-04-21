using Eco.Plugins.Networking.Clients;
using Eco.Shared.Networking;
using Eco.Shared.Serialization;
using QQLib.Event.GeneralEvents;
using QQLib.Event.GeneralEvents.ClientUpdateEvents;
using QQLib.Event.GeneralEvents.RPCEvent;
using QQLib.Event.GeneralEvents.RpcResponseEvents;
using QQLib.EventsTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQLib.Injection
{
    public class FakeEventHandler : INetworkEventHandler
    {
        private ClientManager oldManager;

        public FakeEventHandler()
        {

        }

        protected internal void registerOldManager(ClientManager old)
        {
            this.oldManager = old;
        }

        public void ReceiveEvent(INetClient client, NetworkEvent netEvent, BSONObject bson)
        {
            switch (netEvent)
            {
                case NetworkEvent.ClientUpdate:
                    if (!createFromNetworkEvent(client, netEvent, bson).isCanceled)
                    NetObjectManager.Obj.UpdateObjects(bson);
                    break;

                case NetworkEvent.RPC:
                    if (!createEventFromNetworkRPCEvent(client, netEvent, bson).isCanceled)
                        RPCManager.HandleReceiveRPC(client, bson);
                    break;

                case NetworkEvent.RPCResponse:
                    if(!createFromNetworkResponseEvent(client, netEvent, bson).isCanceled)
                        RPCManager.HandleQueryResponse(bson);
                   
                    break;
            }
        }

        public void SendEvent(NetworkEvent netEvent, BSONObject bsonObj, INetClient target, INetObject netObj)
        {
            oldManager.SendEvent(netEvent, bsonObj, target, netObj);
        }


        private Event.Event createFromNetworkEvent(INetClient client, NetworkEvent netEvent, BSONObject bson)
        {
            return FireEventTool.call(new ClientUpdateEvent(client, netEvent, bson));
        }

        private Event.Event createFromNetworkResponseEvent(INetClient client, NetworkEvent netEvent, BSONObject bson)
        {
            return FireEventTool.call(new RPCResponseEvent(client, netEvent, bson));
        }



        private Event.Event createEventFromNetworkRPCEvent(INetClient client, NetworkEvent netEvent, BSONObject bson)
        {
            BSONValue value;
            bson.TryGetValue("method", out value);
            string methodname = value.StringValue;

            switch (methodname)
            {
                case "KeepAlive":
                    return FireEventTool.call(new KeepAliveEvent(client, netEvent, bson, methodname));
                case "PlayerInteract":
                    return FireEventTool.call(new PlayerInteract(client, netEvent, bson, methodname));
                default:
                   return FireEventTool.call(new NotregistredRPCMethodEvent(client,netEvent,bson,methodname));

            }

            return null;
        }
    }
}
