using UnityEngine;

public class CleansePlayer : MonoBehaviour
{
    Ability_Schizo playerToCleanse;
    // Start is called before the first frame update
    void Start()
    {
        playerToCleanse = FindAnyObjectByType<Ability_Schizo>();


    }
    public void CleansPLayer()
    {
        playerToCleanse.EnemyTimer = 0;
        playerToCleanse.slider.value = 0;
        Debug.Log("CLEANSING");
    }
}
