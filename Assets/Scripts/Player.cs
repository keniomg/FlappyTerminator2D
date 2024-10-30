using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInput), typeof(PlayerMover))]
[RequireComponent(typeof(PlayerHealth), typeof(PlayerProjectileSpawner), typeof(AttackerData))]
[RequireComponent (typeof(PlayerCollisionHandler), typeof(PlayerStatus), typeof(PlayerAnimator))]
[RequireComponent(typeof(Animator))]

public class Player : Unit
{
    private PlayerMover _playerMover;
    private PlayerInput _playerInput;
    private PlayerHealth _playerHealth;
    private PlayerProjectileSpawner _playerProjectileSpawner;
    private AttackerData _attackerData;
    private UnitStatusEventInvoker _unitStatusEventInvoker;
    private PlayerStatus _playerStatus;
    private PlayerAnimator _playerAnimator;

    private Rigidbody2D _rigidbody;

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

    private void GetComponents()
    {
        _unitStatusEventInvoker = ScriptableObject.CreateInstance<UnitStatusEventInvoker>();

        _playerMover = GetComponent<PlayerMover>();
        _playerInput = GetComponent<PlayerInput>();
        _playerHealth = GetComponent<PlayerHealth>();
        _playerProjectileSpawner = GetComponent<PlayerProjectileSpawner>();
        _attackerData = GetComponent<AttackerData>();
        _playerStatus = GetComponent<PlayerStatus>();
        _playerAnimator = GetComponent <PlayerAnimator>();

        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void InitializeComponents()
    {
        _playerMover.Initialize(_playerInput, _rigidbody, _unitStatusEventInvoker);
        _playerHealth.Initialize(_unitStatusEventInvoker);
        _playerStatus.Initialize(_unitStatusEventInvoker);
        _playerAnimator.Initialize(_playerStatus);
        _playerProjectileSpawner.Initialize(_unitStatusEventInvoker, _attackerData);
    }
}