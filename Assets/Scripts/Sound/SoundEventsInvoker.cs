using System;
using UnityEngine;

[CreateAssetMenu]
public class SoundEventsInvoker : ScriptableObject
{
    public event Action<SoundTypes> SoundRequired;

    public void Invoke(SoundTypes soundType)
    {
        SoundRequired?.Invoke(soundType);
    }
}