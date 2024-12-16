using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private float _moveInputX;
    private float _moveInputY;
    private float _lookInputX;
    private float _lookInputY;

    public Vector2 MoveInputDirection {get; private set; }
    public Vector2 LookInputDirection { get; private set; }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        HandleMoveInput();
        HandleLookInput();
    }

    private void HandleMoveInput()
    {
        _moveInputX = Input.GetAxis("Horizontal");
        _moveInputY = Input.GetAxis("Vertical");
        MoveInputDirection = new(_moveInputX, _moveInputY);
    }

    private void HandleLookInput()
    {
        _lookInputX = Input.GetAxis("Mouse X");
        _lookInputY = Input.GetAxis("Mouse Y");
        LookInputDirection = new(_lookInputX, _lookInputY);
    }
}