using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UnitStatusEventInvoker : ScriptableObject
{
    private Dictionary<int, Action<GameObject, UnitStatusTypes>> _statusEvents = new();

    public void Register(int ID, Action<GameObject, UnitStatusTypes> unitStatusEvent)
    {
        if (!_statusEvents.ContainsKey(ID))
        {
            _statusEvents[ID] = unitStatusEvent;
        }
        else
        {
            _statusEvents[ID] += unitStatusEvent;
        }
    }

    public void Unregister(int ID, Action<GameObject, UnitStatusTypes> unitStatusEvent) 
    {
        if (_statusEvents.ContainsKey(ID))
        {
            _statusEvents[ID] -= unitStatusEvent;
        }
    }

    public void Invoke(int ID, GameObject unitObject, UnitStatusTypes unitStatusType)
    {
        if (_statusEvents.ContainsKey(ID))
        {
            _statusEvents[ID]?.Invoke(unitObject, unitStatusType);
        }
    }
}