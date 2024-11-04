using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GeneralCollisionEventInvoker : ScriptableObject
{
    private Dictionary<int, Action<GameObject, GameObject>> _collisionEvents = new Dictionary<int, Action<GameObject, GameObject>>();

    private void OnEnable()
    {
        _collisionEvents.Clear();
    }

    public void Register(int ID, Action<GameObject, GameObject> collisionEvent)
    {
        if (!_collisionEvents.ContainsKey(ID))
        {
            _collisionEvents[ID] = collisionEvent;
        }
        else
        {
            _collisionEvents[ID] += collisionEvent;
        }
    }

    public void Unregister(int ID, Action<GameObject, GameObject> collisionEvent)
    {
        if (_collisionEvents.ContainsKey(ID))
        {
            _collisionEvents[ID] -= collisionEvent;
        }
    }

    public void InvokeEvent(int ID, GameObject firstCollided, GameObject secondCollided)
    {
        if (_collisionEvents.ContainsKey(ID))
        {
            _collisionEvents[ID]?.Invoke(firstCollided, secondCollided);
        }
    }
}