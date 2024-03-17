using UnityEngine;
using UnityEngine.UI;

public class Ability_Schizo : MonoBehaviour
{
    public GameObject OriginalObjects;
    public GameObject propsShift;
    Animator animator;
    public float MaxDuration;
    public Slider slider;
    float timer;
    float enemyTimer;


    public float EnemyTimer
    {
        set
        {
            timer = value;
            enemyTimer = value;
        }
    }


    public GameObject witchEnemy, player;
    public float spawnEnemyInterval;
    public KeyCode myKey;

    void Start()
    {
        animator = GetComponent<Animator>();

    }
    void Update()
    {
        if (Input.GetKeyDown(myKey))
        {
            animator.SetTrigger("Switch");
        }
        AbilityTimer();
        Vector3 playerPos = player.transform.position;

        //%100
        if (timer == MaxDuration)
        {
            //     Debug.Log("spawningEnemy +  100%");

            enemyTimer += 1 * Time.deltaTime;
            if (enemyTimer >= spawnEnemyInterval)
            {
                //    Debug.Log("enemyTimer +  100%");

                playerPos = new Vector3(playerPos.x + Random.Range(-4f, 4f) * 2, playerPos.y, playerPos.z + Random.Range(-4f, 4f) * 2);

                Instantiate(witchEnemy, playerPos, Quaternion.identity);
                enemyTimer = 0;
            }

        }
        //%50
        else if (timer > MaxDuration / 2)
        {
            //       Debug.Log("spawningEnemy +  50%");
            enemyTimer += 1 * Time.deltaTime;
            if (enemyTimer >= spawnEnemyInterval + 2)
            {
                //               Debug.Log("enemyTimer +  50%");

                playerPos = new Vector3(playerPos.x + Random.Range(-4f, 4f) * 2, playerPos.y, playerPos.z + Random.Range(-4f, 4f) * 2);

                Instantiate(witchEnemy, playerPos, Quaternion.identity);
                enemyTimer = 0;
            }


        }
        //%33
        else if (timer > MaxDuration / 3)
        {
            //   Debug.Log("spawningEnemy +  33%");

            enemyTimer += 1 * Time.deltaTime;
            if (enemyTimer >= spawnEnemyInterval + 3)
            {

                //      Debug.Log("enemyTimer +  33%");
                playerPos = new Vector3(playerPos.x + Random.Range(-4f, 4f) * 2, playerPos.y, playerPos.z + Random.Range(-4f, 4f) * 2);

                Instantiate(witchEnemy, playerPos, Quaternion.identity);
                enemyTimer = 0;
            }


        }


    }

    private void AbilityTimer()
    {
        //actavate the ability
        if (timer <= MaxDuration && propsShift.activeSelf == true)
        {
            timer += 1 * Time.deltaTime;
            if (timer > MaxDuration)
            {
                timer = MaxDuration;

            }
            slider.value = timer / MaxDuration;
        }
        /* //deactavate ability
         if (timer >0 && propsShift.activeSelf == false)
         {
             timer -=  1 * Time.deltaTime;
             if (timer < 0)
             {
                 timer = 0;
             }
             slider.value = timer / MaxDuration;

         }
        */

        animator.SetFloat("Timer", timer / MaxDuration);



    }

    public void SwitchObject()
    {
        if (propsShift.activeSelf == true)
        {
            propsShift.SetActive(false);
            OriginalObjects.SetActive(true);
        }

        else
        {
            propsShift.SetActive(true);
            OriginalObjects.SetActive(false);
        }

    }


}
