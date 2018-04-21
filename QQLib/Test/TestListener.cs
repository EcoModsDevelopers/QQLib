
using Eco.Shared.Networking;
using Eco.Shared.Serialization;
using QQLib.Event.GeneralEvents;
using QQLib.Event.GeneralEvents.ClientUpdateEvents;
using QQLib.Event.GeneralEvents.RPCEvent;
using QQLib.EventsTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQLib.Test
{
    public class TestListener : IEcoEventListener
    {
        //----------------- A GARDER 
        [RegisterEventAttribute()]
        public void onObjectReady(ServerStartedEvent startEvent)
        {
            Console.WriteLine(QQLib.prefix + "QQLib started.");
        }




        [RegisterEventAttribute()]
        public void onObjectReady(ClientUpdateEvent rpc)
        {
            // Console.WriteLine(QQLib.prefix + "Not find: "+rpc.bson.DebugToString());
            BSONValue updatedObjs;
            if (rpc.bson.TryGetValue("objs", out updatedObjs))
            {
                var array = (BSONArray)updatedObjs;
                for (int i = 0; i < array.Count; i++)
                {
                    BSONObject bsonObj = array[i].ObjectValue;
                    int id = bsonObj["id"];
                    if(rpc.client.ID==(id-1))
                    {
                        //Console.WriteLine("ok");
                    }
                    else
                    {
                     //   Console.WriteLine(rpc.client.ID + " VS " + id);
                       // Console.WriteLine(QQLib.prefix + "Not find: " + rpc.bson.DebugToString());
                    }

                }
            }
        }

    }
}
