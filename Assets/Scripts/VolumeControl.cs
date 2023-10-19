using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private float _changeVolumeSpeed;
    [SerializeField] private AudioSource _audioSource;

    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private Coroutine _alarmJob;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Run()
    {
        _audioSource.Play();

        _alarmJob = StartCoroutine(ChangeVolume(_maxVolume, null));
    }

    public void Stop()
    {
        StopCoroutine(_alarmJob);

        StartCoroutine(ChangeVolume(_minVolume, () => _audioSource.Stop()));
    }

    private IEnumerator ChangeVolume(float volume, Action onComplete)
    {
        while (_audioSource.volume != volume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, volume, _changeVolumeSpeed * Time.deltaTime);

            yield return null;
        }

        onComplete?.Invoke();
    }
}
