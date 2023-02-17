using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController instance;
    [SerializeField] AudioClip[] musicTracks;
    private AudioSource audioSource;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }
    private void Start() {
        //audioSource.PlayOneShot(musicTracks[Random.Range(0,musicTracks.Length)]);
    }
    void Update()
    {
        if(audioSource.isPlaying) return;        
        audioSource.PlayOneShot(musicTracks[Random.Range(0,musicTracks.Length)]);
    }
    public void SetVolume(float value)
    {
        audioSource.volume = value;
        
    }
}
