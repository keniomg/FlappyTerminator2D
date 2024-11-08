using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private KeyCode _attackKey;
    private KeyCode _jumpKey;

    public float Horizontal {get; private set; }
    public bool IsAttackKeyHold {get; private set; }

    public event Action JumpKeyPressed;

    private void Awake()
    {
        _attackKey = KeyCode.Mouse0;
        _jumpKey = KeyCode.Space;
    }

    public void ManageInput()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        IsAttackKeyHold = Input.GetKey(_attackKey);

        if (Input.GetKeyDown(_jumpKey))
        {
            JumpKeyPressed?.Invoke();
        }
    }
}