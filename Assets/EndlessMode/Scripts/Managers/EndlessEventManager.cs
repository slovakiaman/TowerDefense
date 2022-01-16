using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EndlessEventManager : MonoBehaviour
{
    public static EndlessEventManager instance;
    
    public Dictionary<EndlessLevelEventID, EndlessLevelEvent> activeEvents;
    
    private Dictionary<int, bool> conflictingGroupsActive;

    public EventCollection allEvents;
    
    private void Awake()
    {
        instance = this;
        activeEvents = new Dictionary<EndlessLevelEventID, EndlessLevelEvent>();
        conflictingGroupsActive = new Dictionary<int, bool>();
        foreach (EndlessLevelEvent endlessLevelEvent in allEvents.allEvents)
        {
            if (!conflictingGroupsActive.ContainsKey(endlessLevelEvent.conflictingGroup))
            {
                conflictingGroupsActive.Add(endlessLevelEvent.conflictingGroup, false);
            }
        }
    }

    //waveNumber starts at 0
    public void GenerateWaveEvent(int waveNumber)
    {
        UpdateActiveEvents(waveNumber);
        int eventIndex;
        EndlessLevelEvent newEvent;
        int noAttempts = 5;
        
        do
        {
            if (noAttempts <= 0)
                return;
            
            eventIndex = Random.Range(0, allEvents.allEvents.Count);
            newEvent = allEvents.allEvents[eventIndex];
            noAttempts--;
        } while (!conflictingGroupsActive[newEvent.conflictingGroup] && activeEvents.ContainsKey(newEvent.id));

        newEvent.startingWave = waveNumber;
        activeEvents.Add(newEvent.id, newEvent);
        
        EndlessUIManager.instance.ShowEventPanel(newEvent);
    }

    private void UpdateActiveEvents(int waveNumber)
    {
        List<EndlessLevelEvent> active = this.activeEvents.Values.ToList();
        foreach (EndlessLevelEvent endlessLevelEvent in active)
        {
            int remainingDuration = waveNumber - (endlessLevelEvent.startingWave + endlessLevelEvent.duration);
            //update UI?
            if (remainingDuration <= 0)
            {
                activeEvents.Remove(endlessLevelEvent.id);
                conflictingGroupsActive[endlessLevelEvent.conflictingGroup] = false;
            }
        }
    }
    
    //EndlessWaveManager, EndlessUIManager, ... need to implement eventHandling
    
    //use Unity Events?
    //create event that calls method that changes attribute
    //ex: ENEMIES_POWER_UP event has event callback for function WaveManager.EnemiesPowerUp() that sets WaveManager.enemiesPowerUp = true
    //      - attributes are checked every new wave
    // ValueCalculator? - trieda(manazer), ktorý bude vraciať values pre všetky možné veci (ako napr: rýchlosť mobiek, hp mobiek, cost towerky, ...
}