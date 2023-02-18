using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallWithHole : MonoBehaviour
{
    [SerializeField] GameObject normalWall;
    [SerializeField] GameObject wallWithHole;

    [SerializeField] PlayerMovement player;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    void Update()
    {
        // if(player.echoLocation)
        if(player.echoLocation)
        {
            normalWall.SetActive(false);
        }
        else
        {
            normalWall.SetActive(true);
        }
    }
}
