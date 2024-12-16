//using UnityEngine;
//using UnityEngine.InputSystem;

//[RequireComponent(typeof(CharacterController), typeof(PlayerMover), typeof(PlayerGizmosDrawer))]
//[RequireComponent(typeof(PlayerInput), typeof(PlayerInputController), typeof(WakeUper))]
//public class Player : MonoBehaviour
//{
//    [SerializeField] private ShotgunShooter _shotgun = null;

//    private PlayerMover _playerMover;
//    private CharacterController _characterController;
//    private PlayerGizmosDrawer _drawer;
//    private PlayerInputController _inputController;
//    private Collider _collider;

//    private void Awake()
//    {
//        GetComponents();
//        InitializeComponents();
//    }

//    private void GetComponents()
//    {
//        _playerMover = GetComponent<PlayerMover>();
//        _characterController = GetComponent<CharacterController>();
//        _drawer = GetComponent<PlayerGizmosDrawer>();
//        _inputController = GetComponent<PlayerInputController>();
//        _collider = _characterController;
//    }

//    private void InitializeComponents()
//    {
//        _playerMover.Initialize(_characterController, _inputController);
//        _drawer.Initialize(_characterController);

//        if (_shotgun != null)
//        {
//            _shotgun.Initialize(_inputController, _collider);
//        }
//    }
//}