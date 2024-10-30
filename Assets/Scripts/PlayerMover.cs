using UnityEngine;

public class PlayerMover : MonoBehaviour, IMovable
{
    [SerializeField] private float _jumpForceY;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _minimumRotationZ;
    [SerializeField] private float _maximumRotationZ;

    private UnitStatusEventInvoker _unitStatusEventInvoker;
    private PlayerInput _input;
    private Rigidbody2D _rigidbody;
    private Quaternion _minimumRotation;
    private Quaternion _maximumRotation;

    private void Start()
    {
        _minimumRotation = Quaternion.Euler(0, 0, _minimumRotationZ);
        _maximumRotation = Quaternion.Euler(0, 0, _maximumRotationZ);
    }

    private void OnDisable()
    {
        _input.JumpKeyPressed -= Jump;
    }

    public void Initialize(PlayerInput playerInput, Rigidbody2D rigidbody, UnitStatusEventInvoker unitStatusEventInvoker)
    {
        _input = playerInput;
        _input.JumpKeyPressed += Jump;
        _rigidbody = rigidbody;
        _unitStatusEventInvoker = unitStatusEventInvoker;
    }

    public void Move()
    {
        float translationX = _input.Horizontal * _speed * Time.deltaTime;
        float translationY = 0;
        Vector2 translation = new Vector2(translationX, translationY);
        transform.Translate(translation);
        transform.rotation = Quaternion.Lerp(transform.rotation, _minimumRotation, _rotationSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        float jumpForceX = 0;
        Vector2 jumpForce = new(jumpForceX, _jumpForceY);
        transform.rotation = _maximumRotation;
        _rigidbody.velocity = jumpForce;
        _unitStatusEventInvoker.Invoke(gameObject.GetInstanceID(), gameObject, UnitStatusTypes.Jump);
    }
}