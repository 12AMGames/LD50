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

    public AudioMixerGroup musicMixer;
    public AudioMixerGroup vfxMixer;

    public float mainVolume = 1f;
    public float _musicVolume = 1f;
    public float MusicVolume
    {
        get
        {
            return _musicVolume * mainVolume;
        }
        set
        {
            _musicVolume = value;
        }
    }
    public float _vfxVolume = 1f;
    public float VfxVolume
    {
        get
        {
            return _vfxVolume * mainVolume;
        }
        set
        {
            _vfxVolume = value;
        }
    }
    //SliderSubsciber suber;
    Slider mainSlider;
    Slider musicSlider;
    Slider vfxSlider;

    public Sound[] sounds;
    public Sound[] songs;

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
            S.source.outputAudioMixerGroup = vfxMixer;

            S.source.volume = S.volume;
            S.source.pitch = S.pitch;
            S.source.loop = S.loop;
        }

        foreach (Sound S in songs)
        {
            S.source = gameObject.AddComponent<AudioSource>();
            S.source.clip = S.clip;
            S.source.outputAudioMixerGroup = musicMixer;

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

    public void ChangeVolume(int whatVolume)
    {
        switch (whatVolume)
        {
            case 0:
                mainVolume = mainSlider.value;
                PlayerPrefs.SetFloat("mainVol", mainSlider.value);
                break;
            case 1:
                MusicVolume = musicSlider.value;
                PlayerPrefs.SetFloat("musicVol", musicSlider.value);
                break;
            case 2:
                VfxVolume = vfxSlider.value;
                PlayerPrefs.SetFloat("vfxVol", vfxSlider.value);
                break;
        }

        //SoundEffects
        vfxMixer.audioMixer.SetFloat("VfxVol", Mathf.Log10(Mathf.Max(VfxVolume, 0.0001f)) * 20f);
        //MusicTracks
        musicMixer.audioMixer.SetFloat("MusicVol", Mathf.Log10(Mathf.Max(MusicVolume, 0.0001f)) * 20f);
    }

    public void LoadVolumes()
    {
        mainSlider.value = PlayerPrefs.GetFloat("mainVol", 1f);
        musicSlider.value = PlayerPrefs.GetFloat("musicVol", 1f);
        vfxSlider.value = PlayerPrefs.GetFloat("vfxVol", 1f);
        for (int i = 0; i <= 3; i++)
        {
            ChangeVolume(i - 1);
        }
    }

    void VolumeInit()
    {
        ////suber = GameObject.FindGameObjectWithTag("UI").GetComponent<SliderSubsciber>();
        //mainSlider = suber.mainSlider;
        //musicSlider = suber.musicSlider;
        //vfxSlider = suber.vfxSlider;
        LoadVolumes();
    }

    private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
   
    private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        VolumeInit();
    }
}
