using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private SoundEventsInvoker _soundEventInvoker;
    [SerializeField] private AudioSource _projectileSpawned;
    [SerializeField] private AudioSource _projectileCollided;
    [SerializeField] private AudioSource _unitDied;
    [SerializeField] private AudioSource _unitsCollided;
    [SerializeField] private AudioSource _buttonClicked;

    private void OnEnable()
    {
        _soundEventInvoker.SoundRequired += OnSoundRequired;
    }

    private void OnDisable()
    {
        _soundEventInvoker.SoundRequired -= OnSoundRequired;
    }

    private void OnSoundRequired(SoundTypes soundType)
    {
        switch (soundType)
        {
            case SoundTypes.ProjectileSpawned:
                _projectileSpawned.Play();
                break;
            case SoundTypes.ProjectileCollided:
                _projectileCollided.Play();
                break;
            case SoundTypes.UnitDied:
                _unitDied.Play();
                break;
            case SoundTypes.UnitsCollided: 
                _unitsCollided.Play();
                break;
            case SoundTypes.ButtonClicked: 
                _buttonClicked.Play(); 
                break;
        }
    }
}