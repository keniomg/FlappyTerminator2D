using System.Collections;
using System.Collections.Generic;
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

    public PlayerHealth PlayerHealth { get; private set; }
    public UnitStatusEventInvoker UnitStatusEventInvoker { get; private set; }

    private void Awake()
    {
        GetComponents();
        InitializeComponents();
    }

    private void Update()
    {
        _playerMover.Move();
        _playerInput.ManageInput();
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

        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void InitializeComponents()
    {
        _playerMover.Initialize(_playerInput, _rigidbody, UnitStatusEventInvoker);
        PlayerHealth.Initialize(UnitStatusEventInvoker);
        _playerStatus.Initialize(UnitStatusEventInvoker);
        _playerAnimator.Initialize(_playerStatus);
        _playerProjectileSpawner.Initialize(UnitStatusEventInvoker, _attackerData);
    }
}