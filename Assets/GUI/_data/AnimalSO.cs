using UnityEngine;

[CreateAssetMenu(fileName = "AnimalSO", menuName = "Animals/NewAnimalSO", order = 0)]
public class AnimalSO : ScriptableObject {
    public string nameAnimal;
    public Sprite animalIcon;
    [TextArea]
    public string description;
}
