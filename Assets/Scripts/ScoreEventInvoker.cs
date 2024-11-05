using System;
using UnityEngine;

[CreateAssetMenu]
public class ScoreEventInvoker : ScriptableObject 
{
    public event Action<int> ValueChanged;

    public void Invoke(int value)
    {
        ValueChanged?.Invoke(value);
    }
}