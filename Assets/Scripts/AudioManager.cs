using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioType
{
    None = 0,
    FireLaser = 1,
    CollectPowerup = 2,
    Explosion = 3
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip laserSFX;
    [SerializeField] private AudioClip powerupSFX;
    [SerializeField] private AudioClip explosionSFX;

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public void PlayAudio(AudioType audioType)
    {
        AudioClip clip = null;
        float volume = 1f;

        switch (audioType)
        {
            case AudioType.None:
                Debug.Log("AudioType = None???");
                return;
            case AudioType.FireLaser:
                clip = laserSFX;
                volume = 0.7f;
                break;
            case AudioType.CollectPowerup:
                clip = powerupSFX;
                break;
            case AudioType.Explosion:
                clip = explosionSFX;
                break;
        }

        audioSource.PlayOneShot(clip, volume);
    }
}
