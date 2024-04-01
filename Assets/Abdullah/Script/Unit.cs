using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Transform target;
    public float speed = 0.2f;

    Vector3[] path;
    int targetIndex;
    float time = 0;

    private void Start()
    {
        target = GameObject.Find("Player").transform;
    }
    public void FindPath()
    {
        time = time + 1 * Time.deltaTime;
        if (time >= .5f)
        {
            Debug.Log("time" + time);
            time = 0;
            targetIndex = 0;
            PathRequestManger.RequestPath(transform.position, target.position, OnPathFound);
        }
  
    }
    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
            
    {
        if (pathSuccessful)
        {
            StopCoroutine("FollowPath");

            path = newPath;
            StartCoroutine("FollowPath");
        }
    }
   public IEnumerator FollowPath()
    {
        Vector3 currentWayPoint = path[0];

        while (true)
        {
            if (transform.position == currentWayPoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    targetIndex = 0;
                    yield break;
                }
                currentWayPoint = path[targetIndex];
            }
            transform.position = Vector3.MoveTowards(transform.position, currentWayPoint, speed * Time.deltaTime);

            yield return null;
        }
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == targetIndex)
                {

                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }

    }
}
