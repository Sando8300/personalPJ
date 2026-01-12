using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Dictionary<string, AudioClip> Audio;
    public AudioClip[] audioSet;
    public AudioSource source;

    private void Awake()
    {
        Audio = new Dictionary<string, AudioClip>();
    }

    void Start()
    {
        source = GetComponent<AudioSource>();
        for (int i = 0; i<audioSet.Length; i++)
        {
            Audio.Add(audioSet[i].name, audioSet[i]);
        }
       

        GameManagerScript.instance.audioManager = this;
    }

}
