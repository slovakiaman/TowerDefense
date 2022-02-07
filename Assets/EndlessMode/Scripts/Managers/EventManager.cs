namespace EndlessMode.Managers
{
    using EndlessMode.Events;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class EventManager : MonoBehaviour
    {
        public static EventManager instance;
        
        public Dictionary<EventID, Events.Event> activeEvents;
        
        private Dictionary<int, bool> conflictingGroupsActive;

        public EventCollection allEvents;
        
        private void Awake()
        {
            instance = this;
            activeEvents = new Dictionary<EventID, Events.Event>();
            conflictingGroupsActive = new Dictionary<int, bool>();
            foreach (Events.Event actualEvent in allEvents.allEvents)
            {
                if (!conflictingGroupsActive.ContainsKey(actualEvent.conflictingGroup))
                {
                    conflictingGroupsActive.Add(actualEvent.conflictingGroup, false);
                }
            }
        }

        //waveNumber starts at 0
        public void GenerateWaveEvent(int waveNumber)
        {
            UpdateActiveEvents(waveNumber);
            int eventIndex;
            Events.Event newEvent;
            int noAttempts = 5;
            
            do
            {
                if (noAttempts <= 0)
                    return;
                
                eventIndex = Random.Range(0, allEvents.allEvents.Count);
                newEvent = allEvents.allEvents[eventIndex];
                noAttempts--;
            } while (conflictingGroupsActive[newEvent.conflictingGroup] || activeEvents.ContainsKey(newEvent.id));

            newEvent.startingWave = waveNumber;
            activeEvents.Add(newEvent.id, newEvent);

            List<Events.Event> events = activeEvents.Values.ToList();
            UIManager.instance.ShowActiveEvents(events, waveNumber);
            UIManager.instance.UpdateTowerCosts();
        }

        private void UpdateActiveEvents(int waveNumber)
        {
            List<Events.Event> active = this.activeEvents.Values.ToList();
            foreach (Events.Event actualEvent in active)
            {
                int remainingDuration = (actualEvent.startingWave + actualEvent.duration) - waveNumber;
                if (remainingDuration <= 0)
                {
                    activeEvents.Remove(actualEvent.id);
                    conflictingGroupsActive[actualEvent.conflictingGroup] = false;
                }
            }
        }

        public void ResetEventManager()
        {
            activeEvents = new Dictionary<EventID, Events.Event>();
            conflictingGroupsActive = new Dictionary<int, bool>();
            foreach (Events.Event actualEvent in allEvents.allEvents)
            {
                if (!conflictingGroupsActive.ContainsKey(actualEvent.conflictingGroup))
                {
                    conflictingGroupsActive.Add(actualEvent.conflictingGroup, false);
                }
            }
        }
    }   
}