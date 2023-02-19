using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    public static MusicController instance;
    [SerializeField] AudioClip startLVL1;
    [SerializeField] AudioClip endLVL1;
    [SerializeField] AudioClip startLVL2;
    [SerializeField] AudioClip endLVL2;
    [SerializeField] AudioClip startLVL3;
    [SerializeField] AudioClip[] musicTracks;
    private AudioSource audioSource;
    private Transform player;    
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        audioSource = GetComponent<AudioSource>();
        SceneManager.activeSceneChanged += PlayNewSceneMusic;
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
    private void PlayNewSceneMusic(Scene oldScene, Scene newScene)
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Debug.Log(SceneManager.GetActiveScene().buildIndex + " is the scene index");
            audioSource.Stop();
            audioSource.PlayOneShot(startLVL1, 1);
        }
        if(SceneManager.GetActiveScene().buildIndex == 2) {
            Debug.Log(SceneManager.GetActiveScene().buildIndex + " is the scene index");
            audioSource.Stop();
            audioSource.PlayOneShot(startLVL2, 2);
        }
        if(SceneManager.GetActiveScene().buildIndex == 3) {
            Debug.Log(SceneManager.GetActiveScene().buildIndex + " is the scene index");
            audioSource.Stop();
            audioSource.PlayOneShot(startLVL3, 1);
        }
    }
    public void PlayCompleteSceneMusic()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Debug.Log(SceneManager.GetActiveScene().buildIndex + " is the scene index");
            audioSource.Stop();
            audioSource.PlayOneShot(endLVL1, 1);
        }
        if(SceneManager.GetActiveScene().buildIndex == 2) {
            Debug.Log(SceneManager.GetActiveScene().buildIndex + " is the scene index");
            audioSource.Stop();
            audioSource.PlayOneShot(endLVL2, 2);
        }
    }
}
