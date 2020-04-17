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

    private void PopulateDictionaries()
    {
        foreach(AudioClip clip in MusicList)
        {
            // add to music list based on name
            Debug.Log("Adding Clip: " + clip.name);
            Music.Add(clip.name, clip);
        }
        foreach (AudioClip clip in CharacterSFXList)
        {
            // add to music list based on name
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
        Debug.Log("Trying to setthe music to: " + music);
        AudioSource source = this.AudioSources["MusicSource"];
        source.clip = this.Music[music];
        source.loop = true;
        source.Play();
    }

    // These must be updated to reflect the correct source name. Should be passed in as a var
    public void PlayCharacterSFX(string sfx, bool loop = false)
    {
        AudioSource source = this.AudioSources["character"];
        source.clip = this.CharacterSFX[sfx];
        source.loop = loop;
        source.Play();
    }

    public void PlayEnemySFX(string sfx, bool loop = false)
    {
        AudioSource source = this.AudioSources["enemy"];
        source.clip = this.EnemySFX[sfx];
        source.loop = loop;
        source.Play();
    }

    public void PlayWorldSFX(string sfx, bool loop = false)
    {
        AudioSource source = this.AudioSources["world"];
        source.clip = this.WorldSFX[sfx];
        source.loop = loop;
        source.Play();
    }
}
