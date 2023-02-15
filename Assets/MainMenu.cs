using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Slider music;
    [SerializeField] Slider SFX;

    private void Start()
    {
        music.onValueChanged.AddListener(ChangeMusicVolume);
        ChangeMusicVolume(music.value);
        SFX.onValueChanged.AddListener(ChangeSFXVolume);
        ChangeSFXVolume(SFX.value);

    }
    public void ChangeSFXVolume(float value)
    {

    }
    public void ChangeMusicVolume(float value)
    {
        MusicController.instance.SetVolume(value);
    }
}
