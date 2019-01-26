using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    private bool playing1 = false;
    private bool playing2 = false;

    void Awake()
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

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        string sceneName = currentScene.name;

        if(sceneName == "MainMenu" && !playing1)
        {
            playing2 = false;

            Stop("BGM");
            Play("BGM");
            
            playing1 = true;
        }

        if(sceneName == "Level1" && !playing2)
        {
            playing1 = false;

            Stop("BGM");
            Play("BGM");

            playing2 = true;

        }

        foreach(Sound s in sounds)
        {
            s.source.volume = s.volume;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.Name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Play();

    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.Name == name);
        s.source.Stop();
    }

    public void Pause (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.Name == name);
        s.source.Pause();
    }
}
