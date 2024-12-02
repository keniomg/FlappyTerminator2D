using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed = 3;
    [SerializeField] private float _strafeSpeed = 3;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _horizontalTurnSensitivity = 1;
    [SerializeField] private float _verticalTurnSensitivity = 1;
    [SerializeField] private float _verticalMinimumAngle = -89f;
    [SerializeField] private float _verticalMaximumAngle = 89f;
    [SerializeField] private float _jumpSpeed = 7;
    [SerializeField] private float _gravityMultiplier = 2;

    private PlayerInputController _playerInputController;
    private Vector3 _verticalVelocity;
    private CharacterController _characterController;
    private float _cameraAngle = 0;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
        _cameraAngle = _cameraTransform.localEulerAngles.x;
    }

    private void Update()
    {
        Move();
    }

    private void OnDestroy()
    {
        _playerInputController.Jumped -= Jump;
    }

    public void Initialize(CharacterController characterController, PlayerInputController playerInputController)
    {
        _characterController = characterController;
        _playerInputController = playerInputController;
        _playerInputController.Jumped += Jump;
    }

    private void Look()
    {
        _cameraAngle -= _playerInputController.LookInput.y * _verticalTurnSensitivity;
        _cameraAngle = Mathf.Clamp(_cameraAngle, _verticalMinimumAngle, _verticalMaximumAngle);
        _cameraTransform.localEulerAngles = Vector3.right * _cameraAngle;
        _transform.Rotate(Vector3.up * _horizontalTurnSensitivity * _playerInputController.LookInput.x);
    }

    private void Walk()
    {
        Vector3 forward = Vector3.ProjectOnPlane(_cameraTransform.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(_cameraTransform.right, Vector3.up).normalized;

        if (_characterController != null)
        {
            Vector3 playerInput = forward * _playerInputController.MoveInput.y * _speed + right * _playerInputController.MoveInput.x * _strafeSpeed;

            if (_characterController.isGrounded && _verticalVelocity.y <= 0)
            {
                _verticalVelocity = Vector3.down;
                _characterController.Move((playerInput + _verticalVelocity) * Time.deltaTime);
            }
            else
            {
                Vector3 horizontalVelocity = _characterController.velocity;
                horizontalVelocity.y = 0;
                _verticalVelocity += Physics.gravity * _gravityMultiplier * Time.deltaTime;
                _characterController.Move((horizontalVelocity + _verticalVelocity) * Time.deltaTime);
            }
        }
    }

    private void Jump()
    {
        if (_characterController.isGrounded)
        {
            _verticalVelocity = Vector3.up * _jumpSpeed;
        }
    }

    private void Move()
    {
        if (_playerInputController == null)
        {
            return;
        }

        Look();
        Walk();
    }
}