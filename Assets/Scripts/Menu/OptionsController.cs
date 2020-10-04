using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsController : MonoBehaviour
{
    public AudioMixer mainMixer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMainVolume(float value)
    {
        mainMixer.SetFloat("MainVolume", value);
    }
    
    public void SetMusicVolume(float value)
    {
        mainMixer.SetFloat("MusicVolume", value);
    }
}
