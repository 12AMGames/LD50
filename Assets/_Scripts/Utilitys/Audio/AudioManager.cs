using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioMixerGroup mainMixer;

    public float currentVolume = 1f;
    public Sound[] sounds;
    public Sound[] songs;
    bool volOn;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }    
       
        DontDestroyOnLoad(gameObject);

        foreach (Sound S in sounds)
        {
            S.source = gameObject.AddComponent<AudioSource>();
            S.source.clip = S.clip;
            S.source.outputAudioMixerGroup = mainMixer;

            S.source.volume = S.volume;
            S.source.pitch = S.pitch;
            S.source.loop = S.loop;
        }

        foreach (Sound S in songs)
        {
            S.source = gameObject.AddComponent<AudioSource>();
            S.source.clip = S.clip;
            S.source.outputAudioMixerGroup = mainMixer;

            S.source.volume = S.volume;
            S.source.pitch = S.pitch;
            S.source.loop = S.loop;
        }
    }

    private void Start()
    {
        ChangeVolume(0);
    }

    public void Play(string name)
    {
        Sound m = Array.Find(songs, songs => songs.name == name);
        if (m != null)
        {
            m.source.Play();
            return;
        }          
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        if (s == null)
            return;
        s.source.Play();
    }

    public void ChangeSong(string name)
    {
        Sound m = Array.Find(songs, songs => songs.name == name);
        if (m != null)
        {
            Sound isplayin = Array.Find(songs, songs => songs.source.isPlaying == true);
            if(isplayin != null)
            {
              isplayin.source.Stop();
            }
            m.source.Play();
        }
    }

    public void PlayRandSound(string name, int min, int max)
    {
        string integer = Random.Range(min, max).ToString();
        string soundName = name + integer;
        Play(soundName);
    }

    public bool IsPlaying(string name, bool isplaying = false)
    {
        Sound m = Array.Find(songs, songs => songs.name == name);
        if (m != null)
        {
            isplaying = m.source.isPlaying;
            return isplaying;
        }
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        if (s == null)
            return false;
      isplaying = s.source.isPlaying;
        return isplaying;
    }

    public void VolumeToggle()
    {
        if (volOn)
        {
            volOn = false;
            ChangeVolume(0);
        }
        else
        {
            volOn = true;
            ChangeVolume(1);
        }
    }

    public void ChangeVolume(float whatVolume)
    {
        currentVolume = whatVolume;
        mainMixer.audioMixer.SetFloat("Main", Mathf.Log10(Mathf.Max(currentVolume, 0.0001f)) * 20f);
    }

    private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
   
    private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ChangeVolume(0);
    }
}
