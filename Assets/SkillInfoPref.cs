using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SkillInfoPref : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI textMesh;
    public void Init(Sprite image, string text) {
        {
            icon.sprite = image;
            textMesh.text = text;
        }
    }
}
