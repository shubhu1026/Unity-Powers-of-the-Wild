
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickInteract : MonoBehaviour
{
    // Start is called before the first frame update    
    void Start() { }

    // Update is called once per frame    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //Interact with NPC
                if (hit.transform.tag == "NPC")
                {
                    //Debug.Log(hit.transform.parent);
                    Debug.Log(hit.transform.gameObject);
                    Debug.Log(hit.transform.GetChild(0));
                    Debug.Log(hit.transform.GetChild(0).GetComponent<AudioSource>());
                    hit.transform.GetChild(0).GetComponent<AudioSource>().Play();
                }
            }
        }
    }
}
