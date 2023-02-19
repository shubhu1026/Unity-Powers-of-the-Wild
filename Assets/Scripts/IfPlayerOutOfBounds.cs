using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfPlayerOutOfBounds : MonoBehaviour
{
    private void Update() {
        if(transform.position.y < -20f)
        {
            GameManager.Instance.RestartLevel();
        }
    }
}
