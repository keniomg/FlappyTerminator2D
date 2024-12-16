using UnityEngine;

public class ShotgunSounder : MonoBehaviour
{
    [SerializeField] private ShotgunShooter _shotgunShooter;
    [SerializeField] private AudioSource _shootSound;
    [SerializeField] private AudioSource _reloadSound;
    [SerializeField] private float _pitchDownValue;

    private float _defaultPitch;

    private void Awake()
    {
        _defaultPitch = _shootSound.pitch - _pitchDownValue;
    }

    private void OnEnable()
    {
        _shotgunShooter.Shooted += PlayShotSound;
        _shotgunShooter.ReloadStarted += PlayReloadSound;
    }

    private void OnDisable()
    {
        _shotgunShooter.Shooted -= PlayShotSound;
        _shotgunShooter.ReloadStarted -= PlayReloadSound;
    }

    private void PlayShotSound()
    {
        GetPitchedAudioSource(_shootSound).Play();
    }

    private void PlayReloadSound()
    {
        _reloadSound.Play();
    }

    private AudioSource GetPitchedAudioSource(AudioSource audioSource)
    {
        float pitchDeviationValue = 0.1f;
        float pitchValue = Random.Range(_defaultPitch + pitchDeviationValue, _defaultPitch - pitchDeviationValue);
        audioSource.pitch = pitchValue;

        return audioSource;
    }
}