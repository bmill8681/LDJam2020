using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    public static AudioManagerScript Instance { get; private set; }

    public List<AudioClip> MusicList;
    public List<AudioClip> CharacterSFXList ;
    public List<AudioClip> EnemySFXList;
    public List<AudioClip> WorldSFXList ;
    public List<AudioSource> AudioSourceList;
    [Range(0.0f, 1.0f)]
    public float MusicVolumeSlider;
    [Range(0.0f, 1.0f)]
    public float SFXVolumeSlider;

    private Dictionary<string, AudioClip> Music = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> CharacterSFX = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> EnemySFX = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> WorldSFX = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioSource> AudioSources = new Dictionary<string, AudioSource>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            PopulateDictionaries();
        } else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(AudioSources["MusicSource"].volume != MusicVolumeSlider)
        {
            AudioSources["MusicSource"].volume = MusicVolumeSlider;
        }
        if (AudioSources["CharacterSFXSource"].volume != SFXVolumeSlider)
        {
            AudioSources["CharacterSFXSource"].volume = SFXVolumeSlider;
        }
    }

    private void PopulateDictionaries()
    {
        foreach(AudioClip clip in MusicList)
        {
            // add to music list based on name
            Music.Add(clip.name, clip);
        }
        foreach (AudioClip clip in CharacterSFXList)
        {
            // add to music list based on name
            CharacterSFX.Add(clip.name, clip);
        }
        foreach (AudioClip clip in EnemySFXList)
        {
            // add to music list based on name
        }
        foreach (AudioClip clip in WorldSFXList)
        {
            // add to music list based on name
        }
        foreach(AudioSource src in AudioSourceList)
        {
            // add to sources based on name
            AudioSources.Add(src.name, src);
        }
    }

    public void SetMusic(string music)
    {
        AudioSource source = this.AudioSources["MusicSource"];
        source.clip = this.Music[music];
        source.loop = true;
        source.volume = MusicVolumeSlider;
        source.Play();
    }

    // This should be refactored - maybe use delegates

    public void PlayCharacterSFX(string sfx, string src, bool loop = false)
    {
        PlaySFXAudio(this.CharacterSFX[sfx], src, loop);
    }

    public void PlayEnemySFX(string sfx, string src, bool loop = false)
    {
        PlaySFXAudio(this.EnemySFX[sfx], src, loop);
    }

    public void PlayWorldSFX(string sfx, string src, bool loop = false)
    {
        PlaySFXAudio(this.WorldSFX[sfx], src, loop);
    }

    private void PlaySFXAudio(AudioClip clip, string src, bool loop)
    {
        AudioSource source = this.AudioSources[src];
        source.clip = clip;
        source.loop = loop;
        source.Play();
    }
}
