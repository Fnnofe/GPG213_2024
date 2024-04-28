using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class BoidsBehaviour : MonoBehaviour
{
    Vector3 cohesionMove;
    Vector3 seperationMove;
    Vector3 alignmentMove;
    Vector3 followPlayer;

    public void Awake()
    {
        
    }
    public Vector3 CalculateBehaviour(Entity entity, List<Transform> otherEntites, BoidsManager manager, Transform target) {


        Seperate(entity, otherEntites, manager, target);
        Alighment(entity, otherEntites, manager, target);
        Cohesion(entity, otherEntites, manager, target);
        FollowPlayer(entity, otherEntites, manager, target);

        // FollowPlayer(entity, otherEntites, manager, target);
        Vector3 move = cohesionMove * BoidsManager.Instance.cohesionWeight + seperationMove * BoidsManager.Instance.seperationWeight+ alignmentMove * BoidsManager.Instance.alignmentWeight + followPlayer * BoidsManager.Instance.chaseWeight;
        Debug.Log("moveBOIDS" + move);

        return move;
    }


    public Vector3 Cohesion(Entity entity,List<Transform>otherEntites ,BoidsManager manager, Transform target)
    {
        cohesionMove = Vector3.zero;
        //case nothing around us
        if (otherEntites.Count == 0) return Vector3.zero;
        //case other Entity are around us
        foreach (Transform detectedEntity in otherEntites) cohesionMove += detectedEntity.position;
        //average all the position
        cohesionMove /= otherEntites.Count;

        // direction of movement from the indivsual boid
        cohesionMove -= entity.transform.position;

        cohesionMove.y = 0;
        Debug.Log("cohesionMove" + otherEntites.Count);
        Debug.Log("otherEntites.Count" + otherEntites.Count);

        return cohesionMove;

    }
    public Vector3 Seperate(Entity entity, List<Transform> otherEntites, BoidsManager manager, Transform target)
    {
        int avoidOthers = 0;
        if (otherEntites.Count == 0) return Vector3.forward;
         seperationMove = Vector3.zero;
        foreach (Transform detectedEntity in otherEntites)
        {
            if (Vector3.SqrMagnitude(detectedEntity.position - entity.transform.position) < manager.SquareAvoidanceRadius)
            {
                avoidOthers++;
                seperationMove += (entity.transform.position - detectedEntity.position);

            }

        }

        if (avoidOthers > 0) seperationMove /= avoidOthers;
        seperationMove.y = 0;
        Debug.Log("seperationMove" + seperationMove);
                Debug.Log("otherEntites.Count" + otherEntites.Count);

        return seperationMove;





    }
    public Vector3 Alighment(Entity entity, List<Transform> otherEntites, BoidsManager manager, Transform target)
    {
        Vector3 alignmentMove = Vector3.forward;
        if (otherEntites.Count == 0) return entity.transform.forward;
        foreach (Transform detectedEntity in otherEntites) alignmentMove += detectedEntity.forward;
        //average all the position
        alignmentMove /= otherEntites.Count;
        alignmentMove.y = 0;
        Debug.Log("alignmentMove" + alignmentMove);

        return alignmentMove;
    }
    public  Vector3 FollowPlayer(Entity entity, List<Transform> otherEntites, BoidsManager manager, Transform target)
    {
        followPlayer = target.position - entity.transform.position;
        followPlayer.y = 0f;
        followPlayer.Normalize();
        return followPlayer;


    }


}
