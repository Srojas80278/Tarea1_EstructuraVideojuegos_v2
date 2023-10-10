using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField]
    Sound[] sounds;

    AudioSource _musicSource;

    AudioSource _sfxSource;

    protected override void Awake()
    {
        base.Awake();

        if (_musicSource == null && _sfxSource == null)
        {
            _musicSource = gameObject.AddComponent<AudioSource>();
            _sfxSource = gameObject.AddComponent<AudioSource>();
        }
    }

    Sound FindSound(string name)
    {
        return Array.Find(sounds, s => s.name == name);
    }

    public void PlayMusic(string name)
    {
        Sound music = FindSound(name);
        if (music != null)
        {
            _musicSource.loop = true;
            _musicSource.clip = music.sound;
            _musicSource.Play();
        }
    }

    public void PlaySFX(string name, bool loop = true)
    {
        Sound sfx = FindSound(name);
        if (sfx != null)
        {
            if (!loop)
            {
                _sfxSource.PlayOneShot(sfx.sound);
            }
            else
            {
                _sfxSource.loop = loop;
                _sfxSource.clip = sfx.sound;
                _sfxSource.Play();
            }
        }
    }

    public void StopSfx()
    {
        _sfxSource.Stop();
    }
}
