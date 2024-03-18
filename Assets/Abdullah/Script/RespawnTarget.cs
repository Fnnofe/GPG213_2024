using UnityEngine;

public class RespawnTarget : MonoBehaviour
{
    public Transform[] respawningPosition;
    [Range(0, 100)]
    public int distanceFromGround;
    void LateUpdate()
    {

        if (gameObject.transform.position.y < distanceFromGround * -1)
        {
            Transform closetPosition = null;

            foreach (Transform t in respawningPosition)
            {
                if (!t) continue;
                if (closetPosition == null) closetPosition = t;
                Vector3 storedPosition = gameObject.transform.position - closetPosition.position;
                Vector3 newPosition = gameObject.transform.position - t.position;

                Debug.Log(" newPosition" + newPosition.magnitude);
                Debug.Log(" storedPosition" + storedPosition.magnitude);
                if (newPosition.magnitude < storedPosition.magnitude)
                {
                    Debug.Log(" closetPosition" + t.name);
                    closetPosition = t;
                }

            }

            transform.position = closetPosition.position;



        }
    }
}
