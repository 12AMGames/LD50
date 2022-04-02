using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;
    public AudioMixerGroup mixer;

    [Range(.1f, 3)]
    public float pitch = 1f;
    [Range(0, 1)]
    public float volume = 1f;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
