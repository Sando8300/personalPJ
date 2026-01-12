using UnityEngine;
using UnityEngine.Audio;

public class SoundSetting : MonoBehaviour
{
    public AudioMixer mixer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mixer = GetComponent<AudioMixer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
