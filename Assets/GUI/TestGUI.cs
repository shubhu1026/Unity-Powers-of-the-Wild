using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGUI : MonoBehaviour
{
    [SerializeField] private AnimalSO[] animalSkillsSO;
    void Start()
    {
        SkillsButtonsCreator.instance.CreateSkillButtons(animalSkillsSO);
    }

    
}
