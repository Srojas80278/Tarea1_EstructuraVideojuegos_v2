using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Sound
{
    [SerializeField]
    public string name;

    [SerializeField]
    public AudioClip sound;
}
