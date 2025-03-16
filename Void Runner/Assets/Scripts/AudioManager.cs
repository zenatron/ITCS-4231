using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;
using System;

public class AudioManager : MonoBehaviour
{
    /* Phil 3/4/2025
    AudioManager script
    We use AudioMixer and SFX Pooling
    */
#region Audio Parameters
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource _bgmSource;
    [SerializeField] private AudioSource _rollSource;

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private string bgmVolumeParameter = "BGMVolume";
    [SerializeField] private string sfxVolumeParameter = "SFXVolume";

    [Header("Volume Controls (dB)")]
    [SerializeField, Range(-80f, 0f)] private float bgmVolume = 0f;
    [SerializeField, Range(-80f, 0f)] private float sfxVolume = 0f;
    [SerializeField, Range(0f, 1f)] private float rollSFXVolume = 1f;


    [Header("SFX Pooling")]
    [SerializeField] private int initialSFXPoolSize = 5;
    private List<AudioSource> sfxSourcesPool = new List<AudioSource>();

    [Header("Audio Clips")]
    [SerializeField] private AudioClip _bgmClip;
    [SerializeField] private AudioClip _collisionClip;
    [SerializeField] private AudioClip _rollingClip;
    [SerializeField] private AudioClip _deathClip;
    [SerializeField] private AudioClip _checkpointClip;

    // More clips to be used in the future
    // [SerializeField] private AudioClip _abilitySwitchClip;
    // [SerializeField] private AudioClip _winClip;
#endregion Audio Parameters

    // Singleton instance that persists between scenes
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        InitializeSFXPool();
        PlayBackgroundMusic();

        SetBGMVolume(bgmVolume);
        SetSFXVolume(sfxVolume);
    }

    private void OnValidate()
    {
        if (_audioMixer)
        {
            SetBGMVolume(bgmVolume);
            SetSFXVolume(sfxVolume);
        }
        if (_rollSource)
        {
            _rollSource.volume = rollSFXVolume;
        }
    }

    /// <summary>
    /// Initializes the pool of SFX AudioSources.
    /// </summary>
    private void InitializeSFXPool()
    {
        for (int i = 0; i < initialSFXPoolSize; i++)
        {
            CreateNewSFXSource();
        }
    }

    /// <summary>
    /// Creates a new AudioSource for SFX, sets its output group, and adds it to the pool.
    /// </summary>
    private AudioSource CreateNewSFXSource()
    {
        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        // Route this AudioSource to the SFX group.
        AudioMixerGroup[] groups = _audioMixer.FindMatchingGroups("SFX");
        if (groups.Length > 0)
        {
            newSource.outputAudioMixerGroup = groups[0];
        }
        newSource.playOnAwake = false;
        sfxSourcesPool.Add(newSource);
        return newSource;
    }

    /// <summary>
    /// Returns an available (non-playing) AudioSource from the pool. Creates one if needed.
    /// </summary>
    private AudioSource GetAvailableSFXSource()
    {
        foreach (AudioSource source in sfxSourcesPool)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }
        return CreateNewSFXSource();
    }

#region SFX & BGM Playback Methods
    /// <summary>
    /// Plays the background music continuously.
    /// </summary>
    public void PlayBackgroundMusic()
    {
        if (_bgmSource && _bgmClip)
        {
            _bgmSource.clip = _bgmClip;
            _bgmSource.loop = true;
            // Route BGM to its group.
            AudioMixerGroup[] groups = _audioMixer.FindMatchingGroups("BGM");
            if (groups.Length > 0)
            {
                _bgmSource.outputAudioMixerGroup = groups[0];
            }
            _bgmSource.Play();
        }
    }

    /// <summary>
    /// Plays a single SFX clip
    /// </summary>
    public void PlaySFX(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("No audio clip provided for: " + clip.name);
            return;
        }
        AudioSource source = GetAvailableSFXSource();
        source.PlayOneShot(clip);
    }

    /// <summary>
    /// Play collision SFX
    /// </summary>
    public void PlayCollisionSFX()
    {
        PlaySFX(_collisionClip);
    }

    /// <summary>
    /// Play roll SFX
    /// May need a better implementation to loop the sound
    /// </summary>
    public void PlayRollingSFX()
    {
        PlaySFX(_rollingClip);
    }

    public void StartRollingSFX()
    {
        if (_rollSource != null && !_rollSource.isPlaying)
        {
            _rollSource.clip = _rollingClip;
            _rollSource.loop = true;
            _rollSource.volume = rollSFXVolume;
            _rollSource.Play();
        }
    }

    public void StopRollingSFX()
    {
        if (_rollSource != null && _rollSource.isPlaying)
        {
            _rollSource.Stop();
        }
    }

    /// <summary>
    /// Play death SFX
    /// </summary>
    public void PlayDeathSFX()
    {
        PlaySFX(_deathClip);
    }

    /// <summary>
    /// Play checkpoint SFX
    /// </summary>
    public void PlayCheckpointSFX()
    {
        PlaySFX(_checkpointClip);
    }

#endregion SFX & BGM Playback Methods


#region Volume Adjustments
    /// <summary>
    /// Adjusts volume for BGM
    /// Expected volume in decibels
    /// </summary>
    public void SetBGMVolume(float volume)
    {
        _audioMixer.SetFloat(bgmVolumeParameter, volume);
    }

    /// <summary>
    /// Adjusts volume for SFX
    /// Expected volume in decibels
    /// </summary>
    public void SetSFXVolume(float volume)
    {
        _audioMixer.SetFloat(sfxVolumeParameter, volume);
    }
#endregion Volume Adjustments

}