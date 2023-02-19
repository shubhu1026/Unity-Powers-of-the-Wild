using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfPlayerOutOfBounds : MonoBehaviour
{
    private void Update() {
        if(transform.position.y < -30f)
        {
            GameManager.Instance.RestartLevel();
        }
    }
}
