using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Transform _playerSpawnPosition;
    [SerializeField] private Player _playerPrefab;

    private UnitStatusEventInvoker _playerStatusEventInvoker;
    private Player _player;

    private void OnEnable()
    {
        _player = Instantiate(_playerPrefab, _playerSpawnPosition);
        _playerStatusEventInvoker = _player.TryGetComponent(out UnitStatusEventInvoker unitStatusEventInvoker) ? unitStatusEventInvoker : null;
        _playerStatusEventInvoker.Register(_player.GetInstanceID(), OnPlayersGameOver);
    }

    private void OnDisable()
    {
        _playerStatusEventInvoker.Unregister(_player.GetInstanceID(), OnPlayersGameOver);
    }

    private void OnPlayersGameOver(GameObject player, UnitStatusTypes statusType)
    {
        if (statusType is UnitStatusTypes.Die)
        {
            Destroy(_playerPrefab.gameObject);
        }
    }
}