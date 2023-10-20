using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public SoundAudioClip[] soundAudioClipsArray;

    public enum Sounds
    {
        CannonAttack,
        Hit,
        Thunder
    }
    public void PlaySound(Sounds _sound)
    {
        //Create Audio Source Game Object
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();


        audioSource.clip = GetAudioClip(_sound).audioClip;
        audioSource.volume = GetAudioClip(_sound).volume;
        audioSource.PlayOneShot(audioSource.clip);

        Destroy(soundGameObject, audioSource.clip.length);
    }

    private SoundAudioClip GetAudioClip(Sounds _sound)
    {
        foreach(SoundAudioClip soundAudioClip in soundAudioClipsArray)
        {
            if(soundAudioClip.sound == _sound)
            {
                return soundAudioClip;
            }
        }

        Debug.LogError("Sound " + soundAudioClipsArray + "not found!");
        return null;
    }

    [Serializable]
    public class SoundAudioClip
    {
        public Sounds sound;
        public AudioClip audioClip;

        [SerializeField, Range(0f, 1f)]
        public float volume;
    }

}
