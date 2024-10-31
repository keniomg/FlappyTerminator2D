using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Transform _playerSpawnPosition;
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private HealthViewerUIHealthBar _healthBarUI;

    private Player _player;

    private void Awake()
    {
        _player = Instantiate(_playerPrefab, _playerSpawnPosition);
        _player.GetComponents();
        _player.InitializeComponents();
        _healthBarUI.Initialize(_player.PlayerHealth);
    }

    private void OnEnable()
    {
        _player.UnitStatusEventInvoker.Register(_player.gameObject.GetInstanceID(), OnPlayersGameOver);
    }

    private void OnDisable()
    {
        _player.UnitStatusEventInvoker.Unregister(_player.gameObject.GetInstanceID(), OnPlayersGameOver);
    }

    private void OnPlayersGameOver(GameObject player, UnitStatusTypes statusType)
    {
        if (statusType is UnitStatusTypes.Die)
        {
            Destroy(_player.gameObject);
        }
    }
}