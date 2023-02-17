using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] AbilitySet level1Set;
    [SerializeField] AbilitySet level2Set;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RestartLevel()
    {
        SelectAbilitySet(GetLevelFromSceneBuildIndex());

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
                abilitySetToBeUsed = level1Set;
                break;
            case 2:
                abilitySetToBeUsed = level2Set;
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
