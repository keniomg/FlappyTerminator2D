using System;
using UnityEngine;

public class BaseHealth : MonoBehaviour 
{
    [field: SerializeField] public LayerMask Own { get; protected set; }
    [field: SerializeField] public int MaximumValue { get; private set; }

    protected int MinimumValue;

    protected UnitStatusEventInvoker UnitStatusEventInvoker;

    public int CurrentValue { get; protected set; }

    public event Action<GameObject, UnitStatusTypes> StatusChanged;
    public event Action<int, int> ValueChanged;

    protected virtual void OnDisable()
    {
        UnitStatusEventInvoker.Unregister(gameObject.GetInstanceID(), StatusChanged);
    }

    public virtual void Initialize(UnitStatusEventInvoker unitStatusEventInvoker)
    {
        UnitStatusEventInvoker = unitStatusEventInvoker;
        UnitStatusEventInvoker.Register(gameObject.GetInstanceID(), StatusChanged);
    }

    protected void InvokeValueChangedEvent()
    {
        ValueChanged?.Invoke(CurrentValue, MaximumValue);
    }
}