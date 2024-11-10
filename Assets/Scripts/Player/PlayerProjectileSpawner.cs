using UnityEngine;

public class PlayerProjectileSpawner : ProjectileSpawner
{
    private PlayerInput _playerInput;

    protected override void FixedUpdate()
    {
        IsAttackAvailable = _playerInput.IsAttackKeyHold;
        float directionX = Mathf.Cos(gameObject.transform.rotation.z);
        float directionY = Mathf.Sin(gameObject.transform.rotation.z);
        Direction = new(directionX, directionY);

        base.FixedUpdate();
    }

    public void InitializeInput(PlayerInput playerInput)
    {
        _playerInput = playerInput;
    }
}