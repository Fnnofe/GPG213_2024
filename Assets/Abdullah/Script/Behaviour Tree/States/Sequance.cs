using System.Collections.Generic;

namespace BehaviorTree {

    public class Sequance : TreeNode
    {

        public Sequance():base() { }
        public Sequance(List<TreeNode> children) : base(children) { }
        // Start is called before the first frame update
        public override NodeState Evaluate()
        {
            bool anyChildIsRunning = false;
            foreach (TreeNode node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.Failure:
                        state = NodeState.Failure;
                        return state;
                    case NodeState.Sucess:
                        state = NodeState.Sucess;
                        continue;
                    case NodeState.Running:
                        anyChildIsRunning = true;
                        continue;
                    default:
                        state = NodeState.Sucess;
                        return state;
                }
            }
            state = anyChildIsRunning ? NodeState.Running : NodeState.Sucess;
            return state;
        }

    }
}
