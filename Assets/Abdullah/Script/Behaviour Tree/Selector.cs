using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree
{

    public class Selector : TreeNode
    {

        public Selector() : base() { }
        public Selector(List<TreeNode> children) : base(children) { }
        // Start is called before the first frame update
        public override NodeState Evaluate()
        {
            foreach (TreeNode node in children) 
            {
                switch (node.Evaluate())
                {
                    case NodeState.Failure:
                        continue;
                    case NodeState.Sucess:
                        state = NodeState.Sucess;
                        return state;
                    case NodeState.Running:
                        state = NodeState.Running;
                        return state;
                    default:
                        continue;
                }
            }
            state = NodeState.Failure;
            return state;
        }
    }
}
