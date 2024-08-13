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
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioSource musicAudioSource;

    [SerializeField] private AudioClip laserSFX;
    [SerializeField] private AudioClip powerupSFX;
    [SerializeField] private AudioClip explosionSFX;

    private const float defaultMusicVolume = 0.75f;
    private const float defaultSFXVolume = 0.75f;

    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1);
        float savedSFXVolume = PlayerPrefs.GetFloat("MusicVolume", 1);
        musicAudioSource.volume = defaultMusicVolume * savedMusicVolume;
        sfxAudioSource.volume = defaultSFXVolume * savedSFXVolume;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void ToggleMusic(bool setPlaying)
    {
        if (setPlaying)
        {
            musicAudioSource.Play();
        }
        else
        {
            musicAudioSource.Stop();
        }
    }

    public void PlayAudio(AudioType audioType)
    {
        AudioClip clip = null;

        switch (audioType)
        {
            case AudioType.None:
                Debug.Log("AudioType = None???");
                return;
            case AudioType.FireLaser:
                clip = laserSFX;
                break;
            case AudioType.CollectPowerup:
                clip = powerupSFX;
                break;
            case AudioType.Explosion:
                clip = explosionSFX;
                break;
        }

        sfxAudioSource.PlayOneShot(clip);
    }

    public void SetMusicVolume(float volume)
    {
        musicAudioSource.volume = defaultMusicVolume * volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxAudioSource.volume = defaultSFXVolume * volume;
    }
}
