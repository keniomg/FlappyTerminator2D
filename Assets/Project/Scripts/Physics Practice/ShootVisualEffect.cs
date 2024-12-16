using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootVisualEffect : MonoBehaviour
{
    [SerializeField] private GameObject _shootLight;
    [SerializeField] private ParticleSystem _shootParticle;
    [SerializeField] private ShotgunShooter _shotgunShooter;
    [SerializeField] private float _lightTimeValue = 0.1f;
    [SerializeField] private Animator _animator;
    [SerializeField] private string _shootTriggerName;

    private WaitForSeconds _lightTime;

    private void Awake()
    {
        _lightTime = new(_lightTimeValue);
    }

    private void OnEnable()
    {
        _shotgunShooter.Shooted += PlayShootEffect;
    }

    private void OnDisable()
    {
        _shotgunShooter.Shooted -= PlayShootEffect;
    }

    private void PlayShootEffect()
    {
        PlayShootParticle();
        StartCoroutine(PlayLight());
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        _animator.SetTrigger(_shootTriggerName);
    }

    private void PlayShootParticle()
    {
        _shootParticle.Clear();
        _shootParticle.Play();
    }

    private IEnumerator PlayLight()
    {
        _shootLight.gameObject.SetActive(true);

        yield return _lightTime;

        _shootLight.gameObject.SetActive(false);
    }
}