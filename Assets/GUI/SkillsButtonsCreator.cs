using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsButtonsCreator : MonoBehaviour
{    
    [SerializeField] private GameObject buttonSkillPrefab;
    [SerializeField] GameObject createSkillInfo;
    public static SkillsButtonsCreator instance;
    private bool isActive;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            return;
        }
        Destroy(gameObject);
    }
    private void Update() {
        if(!isActive) return;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            isActive = false;
            createSkillInfo.SetActive(false);
        }
    }
    public void CreateSkillButtons(Ability[] listOfAnimalSkills)
    {
        isActive = true;
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Ability skillData in listOfAnimalSkills)
        {
            int index = 1;
            GameObject button = Instantiate(buttonSkillPrefab, transform);
            button.GetComponent<AnimalButton>().Init(skillData, index);
            index++;
        }
        createSkillInfo.GetComponent<CreateSkillInfo>().CreateInfo(listOfAnimalSkills);
    }
}
