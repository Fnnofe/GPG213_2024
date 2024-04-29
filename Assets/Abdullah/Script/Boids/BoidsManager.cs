using System.Collections.Generic;
using UnityEngine;

public class BoidsManager : MonoBehaviour
{
    public static BoidsManager Instance;
    float timer;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public Transform followTarget;
    [Range(1f, 10f)]
    public float weightsMultipler = 5f;
    [Range(1f, 10f)]
    public float sensorRadius = 1.5f;
    [Range(0f, 3f)]
    public List<Entity> entities = new List<Entity>();
    private void Start()
    {
        if (followTarget==null) followTarget = GameObject.FindGameObjectWithTag("Player").transform;

    }
    private void Update()
    {
        foreach (Entity entity in entities)
        {
            List<Transform> otherEntites = GetNearbyObjects(entity);
            Vector3 move = entity.gameObject.GetComponent<BoidsBehaviour>().CalculateBehaviour(entity, otherEntites, Instance, followTarget);
            move*= weightsMultipler;
            entity.Move(move);
        }
    }
    List<Transform> GetNearbyObjects(Entity entity)
    {
        List<Transform> otherEntites = new List<Transform>();
        Collider[] contextColliders = Physics.OverlapSphere(entity.transform.position, sensorRadius, ~7);
        foreach (Collider collider in contextColliders)
        {
            if (collider != entity.entityDetectionRange)
            {
                otherEntites.Add(collider.transform);
            }
        }
        return otherEntites;
    }
}
