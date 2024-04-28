using UnityEngine;

public class Entity : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnEnable()
    {
    //    if (BoidsManager.Instance.entities.Count<6)BoidsManager.Instance.entities.Add(this);

    }
    private void OnDisable()
    {

        //    BoidsManager.Instance.entities.Remove(this);
    }
    private void OnDestroy()
    {
       // BoidsManager.Instance.entities.Remove(this);
    }

    Collider _entityDetectionRange;
    public Collider entityDetectionRange { 
    
        get 
        {
            return _entityDetectionRange; 
        } 
    }

    public void Move(Vector3 direction)
    {
        transform.position= Vector3.MoveTowards(transform.position, direction, Time.deltaTime * 0.4f);

    }

}
