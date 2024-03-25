using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockShift : MonoBehaviour
{
    public GameObject[] blockSet1;
    public GameObject[] blockSet2;
    public float intervalTime = 2f;

    private bool blockSet1Active = true;

    void Start()
    {
        StartCoroutine(ToggleBlocks());
    }

    IEnumerator ToggleBlocks()
    {
        while (true)
        {
            if (blockSet1Active)
            {
                SetBlocksActive(blockSet1, true);
                SetBlocksActive(blockSet2, false);
            }
            else
            {
                SetBlocksActive(blockSet1, false);
                SetBlocksActive(blockSet2, true);
            }

            blockSet1Active = !blockSet1Active;

            yield return new WaitForSeconds(intervalTime);
        }
    }

    void SetBlocksActive(GameObject[] blocks, bool active)
    {
        foreach (GameObject block in blocks)
        {
            block.SetActive(active);
        }
    }
}