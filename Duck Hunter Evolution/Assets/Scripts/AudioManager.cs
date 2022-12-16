using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections;

public class AudioManager : MonoBehaviour{

    public Sound[] sounds;
    public float timeScale;
    public static float musicMult = 1;
    public static float gameMult = 1;


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
        GameVolume(gameMult);
        MusicVolume(musicMult);

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
    public void GameVolume(float volume)
    {
        gameMult = volume;

        foreach (Sound s in sounds)
        {
            if(s.name!="Music")
            {
                s.source.volume = s.volume * gameMult;
            }
        }
    }
    public void MusicVolume(float volume)
    {
        musicMult = volume;

        foreach (Sound s in sounds)
        {
            if(s.name=="Music")
            {
                s.source.volume = s.volume * musicMult;
            }
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
