using System;
using UnityEngine;

public class AnimationsEventInvoker : ScriptableObject
{
    public event Action<AnimationsEventsTypes> AnimationsChanged;

    public void Invoke(AnimationsEventsTypes animationsEventsType)
    {
        AnimationsChanged?.Invoke(animationsEventsType);
    }
}
