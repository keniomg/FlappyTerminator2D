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

    public void Register(int id, Action<GameObject, GameObject> collisionEvent)
    {
        if (_collisionEvents.ContainsKey(id) == false)
        {
            _collisionEvents[id] = collisionEvent;
        }
        else
        {
            _collisionEvents[id] += collisionEvent;
        }
    }

    public void Unregister(int id, Action<GameObject, GameObject> collisionEvent)
    {
        if (_collisionEvents.ContainsKey(id))
        {
            _collisionEvents[id] -= collisionEvent;
        }
    }

    public void InvokeEvent(int id, GameObject firstCollided, GameObject secondCollided)
    {
        if (_collisionEvents.ContainsKey(id))
        {
            _collisionEvents[id]?.Invoke(firstCollided, secondCollided);
        }
    }
}