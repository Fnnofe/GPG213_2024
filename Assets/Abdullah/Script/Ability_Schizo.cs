using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class Ability_Schizo : MonoBehaviour
{
    public GameObject OriginalObjects;
    public GameObject propsShift;
    public GameObject witchEnemy;
    public GameObject player;
    public Slider slider;
    public KeyCode myKey;
    public float MaxDuration;
    public float spawnEnemyInterval;
    public float vignetteIntensityMultiplier = 0.5f; // Intensity multiplier for the vignette effect

    Animator animator;
    float timer;
    float enemyTimer;
    public Volume postProcessingVolume;
    Vignette vignette;

    void Start()
    {
        animator = GetComponent<Animator>();
        timer = 0f;
        enemyTimer = 0f;

        // Initialize vignette effect
        postProcessingVolume = FindObjectOfType<Volume>();
        if (postProcessingVolume.profile.TryGet(out Vignette tempVignette))
        {
            vignette = tempVignette;
        }
    }
    public float EnemyTimer
    {
        set
        {
            timer = value;
            enemyTimer = value;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(myKey))
        {
            animator.SetTrigger("Switch");
        }
        AbilityTimer();
        Vector3 playerPos = player.transform.position;

        // Calculate vignette intensity based on ability timer
        float vignetteIntensity = timer / MaxDuration * vignetteIntensityMultiplier;
        vignette.intensity.value = vignetteIntensity;

        //%100
        if (timer == MaxDuration)
        {
            enemyTimer += 1 * Time.deltaTime;
            if (enemyTimer >= spawnEnemyInterval)
            {
                playerPos = new Vector3(playerPos.x + Random.Range(-4f, 4f) * 2, playerPos.y, playerPos.z + Random.Range(-4f, 4f) * 2);
                Instantiate(witchEnemy, playerPos, Quaternion.identity);
                enemyTimer = 0;
            }
        }
        //%50
        else if (timer > MaxDuration / 2)
        {
            enemyTimer += 1 * Time.deltaTime;
            if (enemyTimer >= spawnEnemyInterval + 2)
            {
                playerPos = new Vector3(playerPos.x + Random.Range(-4f, 4f) * 2, playerPos.y, playerPos.z + Random.Range(-4f, 4f) * 2);
                Instantiate(witchEnemy, playerPos, Quaternion.identity);
                enemyTimer = 0;
            }
        }
        //%33
        else if (timer > MaxDuration / 3)
        {
            enemyTimer += 1 * Time.deltaTime;
            if (enemyTimer >= spawnEnemyInterval + 3)
            {
                playerPos = new Vector3(playerPos.x + Random.Range(-4f, 4f) * 2, playerPos.y, playerPos.z + Random.Range(-4f, 4f) * 2);
                Instantiate(witchEnemy, playerPos, Quaternion.identity);
                enemyTimer = 0;
            }
        }
    }

    private void AbilityTimer()
    {
        // Activate the ability
        if (timer <= MaxDuration && propsShift.activeSelf == true)
        {
            timer += 1 * Time.deltaTime;
            if (timer > MaxDuration)
            {
                timer = MaxDuration;
            }
            slider.value = timer / MaxDuration;
        }
        /* // Deactivate ability
        if (timer > 0 && propsShift.activeSelf == false)
        {
            timer -= 1 * Time.deltaTime;
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
