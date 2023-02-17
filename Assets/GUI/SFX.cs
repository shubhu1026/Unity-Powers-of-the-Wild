using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    public static SFX instance;
    private float volume;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        
    }
    public void PlaySFX(AudioClip audioClip, Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(audioClip, pos, volume);
    }
    public void SetVolume(float value)
    {
        volume = value;
        foreach (var item in FindObjectsOfType<AudioSource>())
        {
            item.volume = value;
        }
    }
}
