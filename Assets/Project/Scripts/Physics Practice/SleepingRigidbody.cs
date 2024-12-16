using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SleepingRigidbody : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.Sleep();
    }
}
