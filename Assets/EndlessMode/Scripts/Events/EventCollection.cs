using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Endless Mode Events/Event")]
public class EventCollection : ScriptableObject
{
    [SerializeField]
    public List<EndlessLevelEvent> allEvents;
}