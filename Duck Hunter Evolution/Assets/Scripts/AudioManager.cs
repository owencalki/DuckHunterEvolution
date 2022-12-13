using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections;

public class AudioManager : MonoBehaviour{

    public Sound[] sounds;
    public float timeScale;


    void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.source=gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        Play("Music", 0);

        StartCoroutine(Birds());
    }

    public void Play (string name,float delay)
    {
        Sound s =Array.Find(sounds, sound => sound.name == name);

        if (s == null) { Debug.LogWarning("Sound" + name + "was not found!"); return; }
        s.source.PlayDelayed(delay);
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null) { Debug.LogWarning("Sound" + name + "was not found!"); return; }
        s.source.Stop();
    }
    public void Volume(float volume)
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = s.volume * volume;
        }


    }

    IEnumerator Birds ()
    {
        float sec = UnityEngine.Random.Range(30,100);
        Play("Birds", 0);
        yield return new WaitForSeconds(sec);
        StartCoroutine(Birds());
    }
}
