using UnityEngine;


[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1;
    [Range(.1f, 3f)]
    public float pitch = 1;

    [HideInInspector]
    public AudioSource source;

    public bool loop = false;
    public bool playOnAwake = false;
    public bool addToAudioMixerGroup = true;//we use this to seprate music from sound ,so on game time pause music doesnt stop
}

