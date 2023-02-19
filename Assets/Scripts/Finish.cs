using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] ParticleSystem ps;
    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(NewScene());
        }
    }
    private void Load()
    {
        GameManager.Instance.LoadNextLevel();
    }
    private IEnumerator NewScene()
    {
        ps.Play();
        MusicController.instance.PlayCompleteSceneMusic();
        yield return new WaitForSeconds(2f);
        Load();
    }
}
