using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class SFXChanged : MonoBehaviour
{
    public void OnSFXVolumeChanged()
    {
        GetComponent<AudioSource>().volume = DataHolder.instance.sfxVolume;
    }
}
