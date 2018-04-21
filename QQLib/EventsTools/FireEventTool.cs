using Eco.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQLib.EventsTools
{
    public class FireEventTool
    {
        protected internal static List<IEcoEventListener> registredListener = new List<IEcoEventListener>();

        public static Event.Event call(Event.Event e)
        {
            foreach (IEcoEventListener classRegistred in FireEventTool.registredListener)
            {
                foreach (var processorMethod in classRegistred.GetType().GetMethods().Where(method => method.HasAttribute<RegisterEventAttribute>()))
                {
                    if (processorMethod.IsStatic)
                    {
                        throw new InvalidOperationException(QQLib.prefix + "Command porcessor method must be not static.");
                    }
                    

                    if (processorMethod.ReturnType != typeof(void))
                    {
                        throw new InvalidOperationException(QQLib.prefix + "Command porcessor method must return a void value and actualy return: " + processorMethod.ReturnType);
                    }
                      

                    var parameters = processorMethod.GetParameters();

                    if (parameters.Length != 1)
                    {
                        throw new InvalidOperationException(QQLib.prefix + "Command porcessor method must take one arg Event object only");
                    }

                    
                    if (!typeof(Event.Event).IsAssignableFrom(parameters[0].ParameterType))
                    {
                        throw new InvalidOperationException(QQLib.prefix + "Command porcessor method must take an Event type on arg and the arg is actualy: " + parameters[0].ParameterType);
                    }
                    

                    if (parameters[0].ParameterType==e.GetType())
                    {
                        List<object> processArgs = new List<object> { e };
                        processorMethod.Invoke(classRegistred, processArgs.ToArray());
                    }
                }
                
            }

            return e;
        }


       
    }
}
