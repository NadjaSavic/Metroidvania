using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;


public class AudoManager : MonoBehaviour
{
    public struct AudioQueueItem
    {
        public Sound sound;
        public Action action;
    }

    public static AudoManager instance;
    public Sound[] sounds;
    private Queue<AudioQueueItem> AudioList = new Queue<AudioQueueItem>();

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else instance = this;

        foreach (var sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.playOnAwake = sound.playOnAwake;
            sound.source.loop = sound.IsLooping;
            sound.source.priority = sound.priority; 
        }
    }

    private void Start()
    {
        Play("BGSounds");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
            return;
        if (!s.isVoiceMail)
        {
            s.source.Play();
        }
        else
        {
            addToQueue(new AudioQueueItem {sound = s});
        }
    }

    public void Play(string name, Action callback)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        if (!s.isVoiceMail)
        {
            s.source.Play();
            StartCoroutine(ExecuteCallbackAfterDelay(callback, s.clip.length));
        }
        else
        {
            addToQueue(new AudioQueueItem {sound = s, action = callback});
        }
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        s.source.Stop();
    }

    private void PlayQueue()
    {
        if (AudioList.Count == 0) return;
        var sound = AudioList.Peek();
        sound.sound.source.Play();
        StartCoroutine(ExecuteCallbackAfterDelay(OnQueueItemEnd,
            sound.sound.clip.length));
    }

    private void addToQueue(AudioQueueItem s)
    {
        AudioList.Enqueue(s);
        if (AudioList.Count == 1) PlayQueue();
    }

    private void OnQueueItemEnd()
    {
        var sound = AudioList.Dequeue();
        sound.action?.Invoke();
        PlayQueue();
    }

    private IEnumerator ExecuteCallbackAfterDelay(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action();
    }
}