using Eco.Shared.Networking;
using Eco.Shared.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQLib.Event.GeneralEvents.ClientUpdateEvents
{

    public class ClientUpdateEvent : Event
    {
        private readonly INetClient _client;
        public INetClient client { get { return this._client; } }

        private readonly NetworkEvent _netEvent;
        public NetworkEvent netEvent { get { return this._netEvent; } }

        private readonly BSONObject _bson;
        public BSONObject bson { get { return this._bson; } }



        public ClientUpdateEvent(INetClient client, NetworkEvent netEvent, BSONObject bson)
        {
            this._client = client;
            this._netEvent = netEvent;
            this._bson = bson;

        }

    }
}
