using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AnimalButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{    
    private AnimalSO animalSO;
    [Header("Time to show tooltip")]  
    [SerializeField] private static float timeToShowTooltip = 1f;
    [Space]
    [Header("Do not touch!")]    
    [SerializeField] Image icon;
    Action onPointer;
    
    public void Init(AnimalSO animalSO)
    {
        this.animalSO = animalSO;
        icon.sprite = animalSO.animalIcon;
        GetComponent<Button>().onClick.AddListener(
                ()=>AudioSource.PlayClipAtPoint(this.animalSO.skillSound, Camera.main.transform.position));
    }
    
    
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("over the button");
        var v = GetComponent<RectTransform>();
        Vector3 position = new Vector3(transform.position.x, transform.position.y, 0);
        Popup.instance.ShowPopup(position, animalSO.description);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        Popup.instance.HidePopup();
        
        
    }
}
