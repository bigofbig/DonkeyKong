using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager current;
    public Sound[] sounds;
    [HideInInspector]
    public bool soundIsOff = false;
    [SerializeField] AudioMixerGroup audioMixergroup;
    void Awake()
    {
        #region Singleton
        if (current != null && current != this)
        {
            Destroy(gameObject);
            return;
        }
        current = this;
        #endregion
        //  DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
            if (s.addToAudioMixerGroup)
                s.source.outputAudioMixerGroup = audioMixergroup;
        }
    }

    public void Play(string clipName, MonoBehaviour script, bool oneShoot = false, bool isMusic = false)
    {
        if (soundIsOff)
        {
            if (!isMusic) return;
            else
                if ((PlayerPrefs.GetInt("Music") == 1)) return;
            //this if groups means--> if sound is off , check if this sound request is a sound not a music , then just dont play it
            //but if its music check if muisc is off or not, then decide.
        }
        Sound requestedSound = Array.Find(sounds, sound => sound.name == clipName);
        if (requestedSound == null) { Debug.Log(clipName + " not Found, maybe a typo!" + " Called from " + script.name); return; }
        if (oneShoot)
            requestedSound.source.PlayOneShot(requestedSound.source.clip);
        else
            requestedSound.source.Play();
    }
    public void Play(string clipName)
    {
        if (soundIsOff) return;
        Sound requestedSound = Array.Find(sounds, sound => sound.name == clipName);
        if (requestedSound == null) { Debug.Log(clipName + " not Found, maybe a typo!" + " Called from "); return; }
        requestedSound.source.Play();
    }
    public void Stop(string clipName)
    {
        if (soundIsOff) return;
        Sound requestedSound = Array.Find(sounds, sound => sound.name == clipName);
        if (requestedSound == null) { Debug.Log(clipName + " not Found, maybe a typo!" + " Called from "); return; }
        requestedSound.source.Stop();
    }
}
