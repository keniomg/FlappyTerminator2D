using System;
using UnityEngine;

[CreateAssetMenu]
public class ScenesEventsInvoker : ScriptableObject
{
    public event Action<ScenesEventsTypes> SceneStatusChanged;

    public void Invoke(ScenesEventsTypes systemEvent)
    {
        SceneStatusChanged?.Invoke(systemEvent);
    }
}