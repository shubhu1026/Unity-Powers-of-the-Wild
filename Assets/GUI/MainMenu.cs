using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField] Slider music;
    [SerializeField] Slider SFXslider;

    private void Start()
    {
        music.onValueChanged.AddListener(ChangeMusicVolume);
        ChangeMusicVolume(music.value);
        SFXslider.onValueChanged.AddListener(ChangeSFXVolume);
        ChangeSFXVolume(SFXslider.value);
        if(SceneManager.GetActiveScene().name != "Start Scene") gameObject.SetActive(false);
    }
    public void ChangeSFXVolume(float value)
    {
        DataHolder.instance.SetSFXVolume(value);
    }
    public void ChangeMusicVolume(float value)
    {
        DataHolder.instance.SetMusicVolume(value);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }
}
