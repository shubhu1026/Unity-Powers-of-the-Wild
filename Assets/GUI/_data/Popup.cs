using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Popup : MonoBehaviour
{
    public static Popup instance;
    [SerializeField] private GameObject popupPrefab;
    private Transform popup;
    private TextMeshProUGUI textMeshPro;
    private void Awake() {
        if(instance == null)
        {
            instance = this;
            popup = Instantiate(popupPrefab).transform;
            popup.gameObject.SetActive(false);
            return;
        }
        Destroy(gameObject);
    }
    public void ShowPopup(Vector3 position, string text)
    {
        popup.gameObject.SetActive(true);
        textMeshPro.text = text;
        popup.transform.position = position;
    }
    public void HidePopup()
    {
        popup.gameObject.SetActive(false);
    }
}
