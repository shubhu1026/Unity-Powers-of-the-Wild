using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] AbilitySet level1Set1;
    [SerializeField] AbilitySet level1Set2;
    [SerializeField] AbilitySet level2Set1;
    [SerializeField] AbilitySet level2Set2;
    [SerializeField] AbilitySet level3Set1;
    [SerializeField] AbilitySet level3Set2;

    public static GameManager Instance;

    public AbilitySet abilitySetToBeUsed;

    void Awake() 
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);     
    }

    void Start()
    {
        Debug.Log("Start Game");
        StartGame();
    }
    
    public void StartGame()
    {
        OnLevelLoad();
    }

    public void OnLevelLoad()
    {
        SelectAbilitySet(GetLevelFromSceneBuildIndex());
        Debug.Log("Ability Set Given:" + abilitySetToBeUsed.name);
    }

    public void RestartLevel()
    {
        SelectAbilitySet(GetLevelFromSceneBuildIndex());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        Debug.Log("Load next level");
        SelectAbilitySet(GetLevelFromSceneBuildIndex() + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    int GetLevelFromSceneBuildIndex()
    {
        switch(SceneManager.GetActiveScene().buildIndex)
        {
            case 1:
                return 1;
            case 2:
                return 2;
            case 3:
                return 3;
            default:
                return 1;
        }
    }

    void SelectAbilitySet(int level)
    {
        switch(level)
        {
            case 1:
                if(Random.value > 0.5)
                    abilitySetToBeUsed = level1Set1;
                else
                    abilitySetToBeUsed = level1Set2;    
                break;
            case 2:
                if(Random.value > 0.5)
                    abilitySetToBeUsed = level2Set1;
                else
                    abilitySetToBeUsed = level2Set2;    
                break;
            case 3:
                if(Random.value > 0.5)
                    abilitySetToBeUsed = level3Set1;
                else
                    abilitySetToBeUsed = level3Set2;    
                break; 
        }
    }
}
