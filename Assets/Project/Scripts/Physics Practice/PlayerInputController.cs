using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;

    public event Action Jumped;
    public event Action Shooted;

    public Vector2 LookInput {get; private set; }
    public Vector2 MoveInput {get; private set; }

    private void Awake()
    {
        _playerInputActions = new();
    }

    private void OnEnable()
    {
        _playerInputActions.Enable();

        _playerInputActions.Player.Look.performed += OnLookPerformed;
        _playerInputActions.Player.Look.canceled += OnLookCanceled;

        _playerInputActions.Player.Move.performed += OnMovePerformed;
        _playerInputActions.Player.Move.canceled += OnMoveCanceled;

        _playerInputActions.Player.Shoot.performed += OnShootPerformed;
        _playerInputActions.Player.Jump.performed += OnJumpPerformed;
    }

    private void OnDisable()
    {
        _playerInputActions.Player.Look.performed -= OnLookPerformed;
        _playerInputActions.Player.Look.canceled -= OnLookCanceled;

        _playerInputActions.Player.Move.performed -= OnMovePerformed;
        _playerInputActions.Player.Move.canceled -= OnMoveCanceled;

        _playerInputActions.Player.Shoot.performed -= OnShootPerformed;
        _playerInputActions.Player.Jump.performed -= OnJumpPerformed;

        _playerInputActions.Disable();
    }

    private void OnLookPerformed(InputAction.CallbackContext context)
    {
        LookInput = context.ReadValue<Vector2>();
    }

    private void OnLookCanceled(InputAction.CallbackContext context)
    {
        LookInput = Vector2.zero;
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        MoveInput = Vector2.zero;
    }

    private void OnShootPerformed(InputAction.CallbackContext context)
    {
        Shooted?.Invoke();
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        Jumped?.Invoke();
    }
}