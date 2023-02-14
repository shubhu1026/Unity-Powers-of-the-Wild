using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    public static Popup instance;
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Image background;
    [SerializeField] private Vector2 borderOffset;
    private void Awake() {
        if(instance == null)
        {
            instance = this;
            
            gameObject.SetActive(false);
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
        textMeshPro.transform.position = new Vector3(
                textMeshPro.transform.position.x,
                textMeshPro.transform.position.y + borderOffset.y,
                textMeshPro.transform.position.z); 
        transform.position = position + (Vector3)borderOffset;
    }

    

    public void HidePopup()
    {
        gameObject.SetActive(false);
    }
}
