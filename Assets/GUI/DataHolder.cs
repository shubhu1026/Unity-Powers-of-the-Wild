using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    public static DataHolder instance;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        
    }
    public float musicVolume;
    public float sfxVolume;
    public void SetSFXVolume(float value)
    {
        sfxVolume = value;
        SFX.instance.SetVolume(value);
    }
    public void SetMusicVolume(float value)
    {
        musicVolume = value;
        MusicController.instance.SetVolume(value);
    }
}
