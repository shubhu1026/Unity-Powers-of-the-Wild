using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreateSkillInfo : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    
    public void CreateInfo(Ability[] abilities)
    {
        if(transform.childCount > 0)
        for (int i = transform.childCount - 1; i >= 0 ; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        foreach (var item in abilities)
        {
            GameObject info = Instantiate(prefab, transform);
            info.GetComponent<SkillInfoPref>().Init(item.icon, item.description);
        }
    }
    
}
