using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidsBehaviour : MonoBehaviour
{
    Vector3 cohesionMove;
    Vector3 seperationMove;
    Vector3 alignmentMove;
    Vector3 followPlayer;
    Vector3 move;
    public float chaseWeight;
    public float cohesionWeight;
    public float seperationWeight;
    public float alignmentWeight;
    public void Awake()
    {
        
    }
    public Vector3 CalculateBehaviour(Entity entity, List<Transform> otherEntites, BoidsManager manager, Transform target) {


        Seperate(entity, otherEntites, manager, target);
        Alighment(entity, otherEntites, manager, target);
        Cohesion(entity, otherEntites, manager, target);
        FollowPlayer(entity, otherEntites, manager, target);

        move = cohesionMove * cohesionWeight + seperationMove * seperationWeight + alignmentMove * alignmentWeight + followPlayer * chaseWeight;
        Debug.Log("moveBOIDS" + move);

        return move;
    }
    public Vector3 Cohesion(Entity entity,List<Transform>otherEntites ,BoidsManager manager, Transform target)
    {
        cohesionMove = Vector3.zero;
        if (otherEntites.Count == 0) return Vector3.zero;
        foreach (Transform detectedEntity in otherEntites) cohesionMove += detectedEntity.position;
        cohesionMove /= otherEntites.Count;
        cohesionMove = entity.transform.position - cohesionMove;
        cohesionMove.y = 0;
        return cohesionMove;
    }
    public Vector3 Seperate(Entity entity, List<Transform> otherEntites, BoidsManager manager, Transform target)
    {
        int avoidOthers = 0;
        if (otherEntites.Count == 0) return Vector3.forward;
         seperationMove = Vector3.zero;
        if (avoidOthers > 0) seperationMove /= avoidOthers;
        foreach (Transform detectedEntity in otherEntites) seperationMove += detectedEntity.position;
        seperationMove /= otherEntites.Count;
        seperationMove -= entity.transform.position;
        seperationMove.y = 0;
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
        followPlayer =  entity.transform.position- target.position;
        followPlayer.y = 0f;
        followPlayer.Normalize();
        return followPlayer;


    }


}
