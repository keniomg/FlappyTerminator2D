using System;
using UnityEngine;

public class ValueEventInvoker : ScriptableObject
{
    public event Action<int> ValueChanged;

    public void Invoke(int value)
    {
        ValueChanged?.Invoke(value);
    }
}
