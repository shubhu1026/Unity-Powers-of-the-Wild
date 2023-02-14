using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsButtonsCreator : MonoBehaviour
{    
    [SerializeField] private GameObject buttonSkillPrefab;
    public static SkillsButtonsCreator instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            return;
        }
        Destroy(gameObject);
    }
    public void CreateSkillButtons(AnimalSO[] listOfAnimalSkills)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        foreach (AnimalSO skillData in listOfAnimalSkills)
        {
            GameObject button = Instantiate(buttonSkillPrefab, transform);
            button.GetComponent<AnimalButton>().Init(skillData);
        }
    }
}
