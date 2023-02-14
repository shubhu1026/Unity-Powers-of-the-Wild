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
    }
    
    
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("over the button");
        Popup.instance.ShowPopup(transform.position, animalSO.description);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        Popup.instance.HidePopup();
        
        
    }
}
