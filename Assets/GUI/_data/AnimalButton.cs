using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AnimalButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("For game designer")]
    [Tooltip("Animal scriptable object with animl data")]
    [SerializeField] private AnimalSO animalSO;
    [SerializeField] private static float timeToShowTooltip = 1f;
    [Space]
    [Header("Do not touch!")]    
    [SerializeField] Image icon;
    Action onPointer;
    private static Coroutine coroutine;
    private void Awake()
    {
        
    }
    private void Start()
    {
        icon.sprite = animalSO.animalIcon;
    }
    
    private static IEnumerator DisplayPopup(Vector3 position, string text)
    {
        yield return new WaitForSeconds(timeToShowTooltip);
        Popup.instance.ShowPopup(position, text);
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("over the button");
        if(coroutine == null) coroutine = StartCoroutine(DisplayPopup(transform.position, animalSO.description));
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        Popup.instance.HidePopup();
        StopAllCoroutines();
        coroutine = null;
    }
}
