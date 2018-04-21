using Eco.Core.Plugins.Interfaces;
using Eco.Plugins.Networking;
using Eco.Plugins.Networking.Clients;
using Eco.Shared.Networking;
using QQLib.Event.GeneralEvents;
using QQLib.EventsTools;
using QQLib.Injection;
using QQLib.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QQLib
{
    public class QQLib : IModKitPlugin, IServerPlugin
    {

        public static String prefix = "QQLib: ";
        private static Boolean started = false;


        public static void registerListener(IEcoEventListener listener)
        {
            if (!FireEventTool.registredListener.Contains(listener))
            {
                FireEventTool.registredListener.Add(listener);
            }
            else
            {
                Console.WriteLine(prefix + "You tried to add a listener twice ! Register canceled.");
            }
        }




        public string GetStatus()
        {
            if (!started)
            {
                onServerFinishedLoaded();
            }
            return "Listeners registred: "+ FireEventTool.registredListener.Count();
        }

        public static void injectNewHandler()
        {

            ClientManager clientManager = ClientManager.Obj;

            FakeEventHandler fakeHandler = new FakeEventHandler();
            fakeHandler.registerOldManager(clientManager);

            var field = typeof(NetObject).GetField("eventHandler", BindingFlags.NonPublic | BindingFlags.Static);
            if(field==null)
            {
                Console.WriteLine(prefix + "Can't find the injection field");
            }
            field.SetValue(null, fakeHandler);


        }

        private static void onServerFinishedLoaded()
        {
            injectNewHandler();
            started = true;
            QQLib.registerListener(new TestListener());

            // FAIRE UN WAIT Pour attendre le chargement des autres plugins 
            FireEventTool.call(new ServerStartedEvent());
        }

        



  
    }
}
