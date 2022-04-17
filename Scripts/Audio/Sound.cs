using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name; 

    public AudioClip clip;
    [Range(0f,1f)]
    public float volume;
    [Range(.1f,3f)]
    public float pitch;

    public bool playOnAwake;
    public bool IsLooping;

    public bool isVoiceMail;

    public int priority; 
    
    [HideInInspector]
    public AudioSource source;

    void Start()
    {
        if (isVoiceMail) source.priority = 3; 
    }

}
