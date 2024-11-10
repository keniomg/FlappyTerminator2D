using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInput), typeof(PlayerMover))]
[RequireComponent(typeof(PlayerHealth), typeof(PlayerProjectileSpawner), typeof(AttackerData))]
[RequireComponent(typeof(PlayerCollisionHandler), typeof(PlayerStatus), typeof(PlayerAnimator))]
[RequireComponent(typeof(Animator))]

public class Player : Unit
{
    private PlayerMover _playerMover;
    private PlayerInput _playerInput;
    private PlayerProjectileSpawner _playerProjectileSpawner;
    private AttackerData _attackerData;
    private PlayerStatus _playerStatus;
    private PlayerAnimator _playerAnimator;
    private Rigidbody2D _rigidbody;
    private PlayerCollisionHandler _playerCollisionHandler;

    public PlayerHealth PlayerHealth { get; private set; }
    public UnitStatusEventInvoker UnitStatusEventInvoker { get; private set; }

    private void Update()
    {
        if (Time.timeScale != 0)
        {
            _playerInput.ManageInput();
            _playerAnimator.HandleAnimation();
        }
    }

    private void FixedUpdate()
    {
        if (Time.timeScale != 0)
        {
            _playerMover.Move();
        }
    }

    public void GetComponents()
    {
        UnitStatusEventInvoker = ScriptableObject.CreateInstance<UnitStatusEventInvoker>();

        _playerMover = GetComponent<PlayerMover>();
        _playerInput = GetComponent<PlayerInput>();
        PlayerHealth = GetComponent<PlayerHealth>();
        _playerProjectileSpawner = GetComponent<PlayerProjectileSpawner>();
        _attackerData = GetComponent<AttackerData>();
        _playerStatus = GetComponent<PlayerStatus>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerCollisionHandler = GetComponent<PlayerCollisionHandler>();

        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void InitializeComponents(ScenesEventsInvoker scenesEventsInvoker, SoundEventsInvoker soundEventsInvoker)
    {
        _playerMover.Initialize(_playerInput, _rigidbody);
        PlayerHealth.Initialize(UnitStatusEventInvoker, scenesEventsInvoker);
        _playerStatus.Initialize(UnitStatusEventInvoker);
        _playerAnimator.Initialize(_playerStatus);
        _playerProjectileSpawner.Initialize(UnitStatusEventInvoker, _attackerData, soundEventsInvoker);
        _playerProjectileSpawner.InitializeInput(_playerInput);
        _playerCollisionHandler.Initialize(PlayerHealth.Own, soundEventsInvoker, UnitStatusEventInvoker);
    }
}