using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private SoundEventsInvoker _soundEventsInvoker;

    private void OnDisable()
    {
        _soundEventsInvoker.Invoke(SoundTypes.UnitDied);
    }
}