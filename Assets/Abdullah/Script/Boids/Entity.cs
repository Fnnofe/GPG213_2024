using UnityEngine;

public class Entity : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnEnable()
    {
       BoidsManager.Instance.entities.Add(this);

    }
    private void OnDisable()
    {

            BoidsManager.Instance.entities.Remove(this);
    }
    private void OnDestroy()
    {
        BoidsManager.Instance.entities.Remove(this);
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
        transform.position= transform.position-direction * Time.deltaTime;

    }

}
