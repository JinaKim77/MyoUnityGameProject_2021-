using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    void Awake()
    {
        //keep the sound even when you are in different scene.
        DontDestroyOnLoad(gameObject);

        foreach ( Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip; 

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        if (instance != null){
            Destroy(gameObject);
            return; 
        } 
    }

    //Background music will always be played in the game scene.
    void Start(){
        Play("BackGroundMusic");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if(s == null)
        {
            Debug.LogWarning("Sound: "+name+" not found!");
            return;
        }
        
        if(Pause.GameIsPaused)
        {
            //s.source.volume *= .5f;    
            s.source.volume=0f;
        }

        s.source.Play();
    }

}