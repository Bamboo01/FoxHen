using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Bamboo.Events
{
    public class EventChannel : UnityEvent<IEventRequestInfo> { }

    public class EventManager : MonoBehaviour
    {
        // Singleton
        static public EventManager Instance
        {
            get;
            private set;
        }
        public void Awake()
        {
            if (Instance)
            {
#if UNITY_EDITOR
                Debug.LogWarning("Event Manager instance already created. Deleting it and instantiating a new instance...");
#endif
                Destroy(Instance);
                Instance = this;
            }
            else
            {
                Instance = this;
            }
        }

        /// <summary>
        /// Stores all the events
        /// </summary>
        Dictionary<string, EventChannel> EventDictionary = new Dictionary<string, EventChannel>();

        /// <summary>
        /// Lists all the methods listening at the moment
        /// </summary>
        Dictionary<string, List<string>> ChannelToListeners = new Dictionary<string, List<string>>();

        /// <summary>
        /// Function to allow an object to listen to a channel, and call a function(s) when a request to said channel is made\
        ///</summary>
        public void Listen(string channelname, UnityAction<IEventRequestInfo> action)
        {
            if (!EventDictionary.ContainsKey(channelname))
            {
                EventDictionary.Add(channelname, new EventChannel());
                ChannelToListeners.Add(channelname, new List<string>());
            }
            EventChannel channel = EventDictionary[channelname];
            channel.AddListener(action);
            ChannelToListeners[channelname].Add("Target: " + action.Target.ToString() + "\nAction: " + action.Method.Name);
        }

        /// <summary>
        /// Allows an object to publish info to all listeners of a channel. Can send custom datatypes over.
        /// </summary>
        public void Publish<T>(string channelname, object sender, T body)
        {
            EventChannel channel;
            if (EventDictionary.TryGetValue(channelname, out channel))
            {
                channel.Invoke(new EventRequestInfo<T>(channelname, sender, body));
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogWarning(
                    string.Format("Tried to publish an event to non-existent event channel \"{0}\" (Did you forget to listen to the channel, or was it a typo?)", channelname)
                );
#endif
            }
        }

        /// <summary>
        /// llows an object to publish info to all listeners of a channel without any data being passed
        /// </summary>
        public void Publish(string channelname, object sender)
        {
            EventChannel channel;
            if (EventDictionary.TryGetValue(channelname, out channel))
            {
                channel.Invoke(new EventRequestInfo(channelname, sender));
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogError("Tried to publish an event to a non-existent event channel with the name " + channelname + " (Did you forget to register a channel, or was it a typo?)");
#endif
            }
        }

        /// <summary>
        /// Allows an object's method to stop listening to a channel
        /// </summary>
        public void Close(string channelname, UnityAction<IEventRequestInfo> action)
        {
            EventChannel channel;
            if (EventDictionary.TryGetValue(channelname, out channel))
            {
                channel.RemoveListener(action);
            }
        }

        /// <summary>
        /// Dump the event manager and see all listeners of every channel
        /// </summary>
        [ContextMenu("Dump event manager")]
        void DumpEventManagerListeners()
        {
#if UNITY_EDITOR
            string message = "===Event Manager dump log===\n";
            foreach (var kvp in ChannelToListeners)
            {

                message += 
                    "Channel: " + "\n" + 
                    kvp.Key + "\n\n" +
                    "Listeners: " + "\n";

                foreach (var str in kvp.Value)
                {
                    message += str + "\n";
                }

                message += "\n";
            }
            Debug.Log(message);
#endif
        }
    }
}