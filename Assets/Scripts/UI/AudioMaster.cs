using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMaster : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixer;

    private bool _isMuteEnabled;
    private int _volumeMiltiplier;
    private float _muteVolume;
    private float _masterVolume;
    private float _musicVolume;
    private float _sfxVolume;
    private float _buttonsVolume;

    private void Awake()
    {
        _muteVolume = -80;
        _volumeMiltiplier = 20;
    }

    private void Update()
    {
        ManageAllSound();
    }

    public void MuteAllSound(bool isEnabled)
    {
        _isMuteEnabled = isEnabled;
    }

    public void ChangeMasterVolume(float volume)
    {
        _masterVolume = volume;
    }

    public void ChangeMusicVolume(float volume)
    {
        _musicVolume = volume;
    }

    public void ChangeButtonsVolume(float volume)
    {
        _buttonsVolume = volume;
    }

    public void ChangeSfxVolume(float volume)
    {
        _sfxVolume = volume;
    }

    private void ManageAllSound()
    {
        if (_isMuteEnabled)
        {
            _audioMixer.audioMixer.SetFloat("Master", _muteVolume);
        }
        else
        {
            ManageMasterVolume();
            ManageMusicVolume();
            ManageButtonsVolume();
            ManageSfxVolume();
        }
    }

    private void ManageMasterVolume()
    {
        _audioMixer.audioMixer.SetFloat("Master", Mathf.Log10(_masterVolume) * _volumeMiltiplier);
    }

    private void ManageMusicVolume()
    {
        _audioMixer.audioMixer.SetFloat("Music", Mathf.Log10(_musicVolume) * _volumeMiltiplier);
    }

    private void ManageButtonsVolume()
    {
        _audioMixer.audioMixer.SetFloat("Buttons", Mathf.Log10(_buttonsVolume) * _volumeMiltiplier);
    }
    
    private void ManageSfxVolume()
    {
        _audioMixer.audioMixer.SetFloat("Sfx", Mathf.Log10(_sfxVolume) * _volumeMiltiplier);
    }
}