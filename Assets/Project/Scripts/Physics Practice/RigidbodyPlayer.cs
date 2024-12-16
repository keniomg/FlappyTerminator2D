using UnityEngine;

[RequireComponent (typeof(Rigidbody), typeof(RigidbodyPlayerMover))]
public class RigidbodyPlayer : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private RigidbodyPlayerMover _rigidbodyPlayerMover;

    private void Awake()
    {
        GetComponents();
        InitializeComponents();
    }

    private void GetComponents()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbodyPlayerMover = GetComponent<RigidbodyPlayerMover>();
    }

    private void InitializeComponents()
    {
        _rigidbodyPlayerMover.Initialize(_rigidbody);
    }
}