using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    
    public enum SEType{
        HitByRubber,
        StretchRubber,
        Barrier,
        BarrierFailed,
        Penalty,
        PushButton,
        ButtonMove,
        Win,
    }

    public enum BGMType{
        Title,
        MainScene,
    }

    [Header("BGM Settings")]
    public List<Sound<BGMType>> bgmSounds; 
    [Header("SE Settings")]
    public List<Sound<SEType>> seSounds;

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float masterVolume = 1f;
    [Range(0f, 1f)] public float musicVolume = 1f; 
    [Range(0f, 1f)] public float seVolume = 1f; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SetupSounds(bgmSounds, SoundType.BGM);
        SetupSounds(seSounds, SoundType.SE);
    }

    private void SetupSounds<T>(List<Sound<T>> sounds, SoundType soundType)
    {
        foreach (Sound<T> sound in sounds)
        {
            GameObject soundObject = new GameObject(sound.soundType.ToString());
            soundObject.transform.SetParent(transform);

            AudioSource audioSource = soundObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            sound.SetSource(audioSource);

            if (soundType == SoundType.BGM)
                audioSource.loop = true;
        }

        UpdateVolumes();
    }

    private void Start()
    {
        UpdateVolumes();
    }

    private void UpdateVolumes()
    {
        foreach (Sound<BGMType> sound in bgmSounds)
        {
            if (sound.source != null)
            {
                sound.source.volume = sound.volume * musicVolume * masterVolume;
            }
        }

        foreach (Sound<SEType> sound in seSounds)
        {
            if (sound.source != null)
            {
                sound.source.volume = sound.volume * seVolume * masterVolume;
            }
        }
    }

    
    public void PlayBGM(BGMType bgmType)
    {
        Sound<BGMType> sound = bgmSounds.Find(s => s.soundType.Equals(bgmType));
        if (sound != null)
        {
            
            sound.Play();
        }
        else
        {
            Debug.LogError("BGM not found: " + bgmType);
        }
    }

    public void PlaySE(SEType seType)
    {
        Sound<SEType> sound = seSounds.Find(s => s.soundType.Equals(seType));
        
        if (sound != null)
        {
            
            sound.PlayOneShot();
        }
        else
        {
            Debug.LogError("SE not found: " + seType);
        }
    }

    public void StopBGM()
    {
        foreach (Sound<BGMType> sound in bgmSounds)
        {
            if (sound.source.isPlaying)
                sound.source.Stop();
        }
    }

    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp01(volume);
        UpdateVolumes();
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        UpdateVolumes();
    }

    public void SetSEVolume(float volume)
    {
        seVolume = Mathf.Clamp01(volume);
        UpdateVolumes();
    }

    private enum SoundType
    {
        BGM,
        SE
    }
}
[System.Serializable]
public class Sound<T>
{
    public T soundType;   // Enumでサウンドを識別
    public AudioClip clip; // 再生するAudioClip
    [Range(0f, 1f)] public float volume = 1f; // 音個別の音量

    [HideInInspector]
    public AudioSource source; // 生成されたAudioSourceを保持（動的に設定される）

    public void SetSource(AudioSource audioSource)
    {
        source = audioSource;
        source.clip = clip;
        source.volume = volume;
    }

    public void Play()
    {
        if (source != null)
        {
            source.Play();
        }
    }

    public void PlayOneShot()
    {
        if (source != null)
        {
            source.PlayOneShot(clip, volume);
        }
    }
}
