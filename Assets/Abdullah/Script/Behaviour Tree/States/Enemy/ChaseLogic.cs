using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using UnityEngine.UIElements;

public class ChaseLogic : TreeNode
{
    public float distanceToPLayer;
    private Transform _transform;
    private Transform player;
    private Animation _animation;
    private Unit _unit;
    private Entity _entity;
    private float _speed;
    float timer;
    public ChaseLogic(Transform transform)
    {
        _transform = transform;
        _animation = transform.GetComponent<Animation>();
        _unit = transform.GetComponent<Unit>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _entity = _transform.GetComponent<Entity>();
        _speed = _unit.speed;
        timer = 2;
    }
    public override NodeState Evaluate()
    {
        Vector3 direction = player.position  - (_transform.position + new Vector3(0, 0.2f, 0));
        Ray ray = new Ray(_transform.position+ new Vector3(0,0.2f,0), direction);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        timer-=1 * Time.deltaTime;

        Debug.Log("hit: "+hit.transform.name);
        if (hit.transform.name == player.transform.name)
        {
            if (direction.magnitude > 1)
            {

                direction.Normalize();
            }
            _unit.StopCoroutine("FollowPath");

            _transform.position = Vector3.MoveTowards(_transform.position, player.position, _speed * Time.deltaTime);


            _transform.position += direction *_speed*Time.deltaTime;
            _transform.rotation = Quaternion.LookRotation(direction);
            Vector3 currentAngle = _transform.rotation.eulerAngles;
            _transform.rotation = Quaternion.Euler(0f, currentAngle.y, currentAngle.z);

            _unit.enabled = false;

            return NodeState.Sucess;
        }
        else
        {
  //          if (timer<=0 ) _entity.enabled = false;
            _unit.enabled = true;
            Debug.Log("Player is Hidding");
            _unit.FindPath();
            return NodeState.Sucess;
        }
    }
}


