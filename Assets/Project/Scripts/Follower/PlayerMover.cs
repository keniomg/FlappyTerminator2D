using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _strafeSpeed;
    [SerializeField] private float _horizontalTurnSensitivity = 1;
    [SerializeField] private float _verticalTurnSensitivity = 1;

    private float _verticalMaximumAngle;
    private float _verticalMinimumAngle;
    private float _cameraAngle;
    private Vector3 _verticalVelocity;
    private Transform _transform;

    private void Awake()
    {
        _verticalMaximumAngle = 89;
        _verticalMinimumAngle = -89;
        _transform = transform;
        _cameraAngle = _cameraTransform.localEulerAngles.x;
    }

    private void Update()
    {
        Look();
        Move();
    }

    private void Look()
    {
        _cameraAngle -= _playerInput.LookInputDirection.y * _verticalTurnSensitivity;
        _cameraAngle = Mathf.Clamp(_cameraAngle, _verticalMinimumAngle, _verticalMaximumAngle);
        _cameraTransform.localEulerAngles = Vector3.right * _cameraAngle;
        _transform.Rotate(Vector3.up * _horizontalTurnSensitivity * _playerInput.LookInputDirection.x);
    }

    private void Move()
    {
        Vector3 forward = Vector3.ProjectOnPlane(_cameraTransform.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(_cameraTransform.right, Vector3.up).normalized;

        if (_characterController != null)
        {
            Vector3 moveDirection = _playerInput.MoveInputDirection.y * forward * _moveSpeed + _playerInput.MoveInputDirection.x * right * _strafeSpeed;

            if (_characterController.isGrounded && _verticalVelocity.y <= 0)
            {
                _verticalVelocity = Vector3.down;
                _characterController.Move((moveDirection + _verticalVelocity) * Time.deltaTime);
            }
            else
            {
                Vector3 horizontalVelocity = _characterController.velocity;
                horizontalVelocity.y = 0;
                _verticalVelocity += Physics.gravity * Time.deltaTime;
                _characterController.Move((horizontalVelocity + _verticalVelocity) * Time.deltaTime);
            }
        }
    }
}