using System;
using UnityEngine;

[CreateAssetMenu]
public class ScoreEventInvoker : ScriptableObject
{
    public event Action<int> ScoreChanged;

    public void Invoke(int value)
    {
        ScoreChanged?.Invoke(value);
    }
}