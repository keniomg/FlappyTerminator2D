using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMaster : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixer;
    [SerializeField] private Slider _allSound;
    [SerializeField] private Slider _music;
    [SerializeField] private Slider _buttons;
    [SerializeField] private Slider _sfx;
    [SerializeField] private Toggle _mute;

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

    private void OnEnable()
    {
        _allSound.onValueChanged.AddListener(ChangeMasterVolume);
        _music.onValueChanged.AddListener(ChangeMusicVolume);
        _buttons.onValueChanged.AddListener(ChangeButtonsVolume);
        _sfx.onValueChanged.AddListener(ChangeSfxVolume);
        _mute.onValueChanged.AddListener(MuteAllSound);
    }

    private void OnDisable()
    {
        _allSound.onValueChanged.RemoveListener(ChangeMasterVolume);
        _music.onValueChanged.RemoveListener(ChangeMusicVolume);
        _buttons.onValueChanged.RemoveListener(ChangeButtonsVolume);
        _sfx.onValueChanged.RemoveListener(ChangeSfxVolume);
    }

    private void Update()
    {
        ManageAllSound();
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

    private void MuteAllSound(bool isEnabled)
    {
        _isMuteEnabled = isEnabled;
    }

    private void ChangeMasterVolume(float volume)
    {
        _masterVolume = volume;
    }

    private void ChangeMusicVolume(float volume)
    {
        _musicVolume = volume;
    }

    private void ChangeButtonsVolume(float volume)
    {
        _buttonsVolume = volume;
    }

    private void ChangeSfxVolume(float volume)
    {
        _sfxVolume = volume;
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