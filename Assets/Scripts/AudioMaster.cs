using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMaster : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixer;

    private bool _isMuteEnabled;
    private float _muteVolume;

    private event Action<float, Channels> VolumeChanged;

    private void Awake()
    {
        _muteVolume = -80;
    }

    private void OnEnable()
    {
        VolumeChanged += OnVolumeChanged;
    }

    private void OnDisable()
    {
        VolumeChanged -= OnVolumeChanged;
    }

    public void MuteAllSound(bool isEnabled)
    {
        _isMuteEnabled = isEnabled;
        VolumeChanged?.Invoke(_muteVolume, Channels.Master);
    }

    public void ChangeMasterVolume(float volume)
    {
        VolumeChanged?.Invoke(volume, Channels.Master);
    }

    public void ChangeMusicVolume(float volume)
    {
        VolumeChanged?.Invoke(volume, Channels.Music);
    }

    public void ChangeButtonsVolume(float volume)
    {
        VolumeChanged?.Invoke(volume, Channels.Buttons);
    }

    public void ChangeSfxVolume(float volume)
    {
        VolumeChanged?.Invoke(volume, Channels.Sfx);
    }

    private enum Channels
    {
        Master,
        Music,
        Buttons,
        Sfx
    }

    private void OnVolumeChanged(float volume, Channels channel)
    {
        if (_isMuteEnabled)
        {
            _audioMixer.audioMixer.SetFloat(Channels.Master.ToString(), _muteVolume);
        }
        else
        {
            ManageChannelVolume(volume, channel);
        }
    }

    private void ManageChannelVolume(float volume, Channels channel)
    {
        _audioMixer.audioMixer.SetFloat(channel.ToString(), Mathf.Log10(volume) * 20);
    }
}
