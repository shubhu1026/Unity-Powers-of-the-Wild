using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    public static Popup instance;
    [SerializeField] float arrowHeight = 40f;
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Image background;
    [SerializeField] private Vector2 borderOffset;
    private void Awake() {
        if(instance == null)
        {
            instance = this;
            
            gameObject.SetActive(false);
            textMeshPro.transform.localPosition = new Vector3(0, borderOffset.y, 0);
            return;
        }
        Destroy(gameObject);
    }
    public void ShowPopup(Vector3 position, string text)
    {
        gameObject.SetActive(true);
        textMeshPro.text = text;
        float width = textMeshPro.preferredWidth;
        float height = textMeshPro.preferredHeight;
        Vector2 size = new Vector2(width + borderOffset.x * 2, height + borderOffset.x * 2);
        background.rectTransform.sizeDelta = size;
        Vector3 newPosition = new Vector3(
                position.x,
                position.y + borderOffset.y/2 + arrowHeight/2,
                position.z); 
        
        transform.position = newPosition;
    }

    

    public void HidePopup()
    {
        gameObject.SetActive(false);
    }
}
