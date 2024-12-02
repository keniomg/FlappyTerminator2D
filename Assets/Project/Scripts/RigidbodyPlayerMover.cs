using UnityEngine;

public class RigidbodyPlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed = 3;

    private Rigidbody _rigidbody;

    private void Update()
    {
        Move();
    }

    public void Initialize(Rigidbody rigidbody)
    {
        _rigidbody = rigidbody;
    }

    public void Move()
    {
        if (_rigidbody != null)
        {
            Vector3 playerInput = new Vector3(Input.GetAxis("Horizontal") * _speed, _rigidbody.velocity.y, Input.GetAxis("Vertical") * _speed);
            
            _rigidbody.velocity = playerInput;
            _rigidbody.velocity += Physics.gravity;
        }
    }
}