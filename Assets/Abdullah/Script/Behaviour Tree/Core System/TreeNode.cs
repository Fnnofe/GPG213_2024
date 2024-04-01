using System.Collections;
using System.Collections.Generic;

namespace BehaviorTree
{
    public enum NodeState
    {
        Running,
        Sucess,
        Failure, 
    }

    public class TreeNode
    {
        protected NodeState state;


        public TreeNode parent;
        protected List<TreeNode> children= new List<TreeNode>();
        private Dictionary<string,object> _dataContext= new Dictionary<string,object>();


        //constructor overloading allow for different initialization scenarios.
        public TreeNode()
        {
            parent = null;
        }
        public TreeNode(List<TreeNode> children)
        {
            foreach (TreeNode child in children)
            {
                _Attach(child);
            }
        }
        private void _Attach(TreeNode node)
        {
            node.parent = this;
            children.Add(node);
        }



        public virtual NodeState Evaluate() => NodeState.Failure;
        public void SetDatat(string key, object value)
        {
            _dataContext[key] = value;
        }
        public object GetDatat(string key)
        {

            object value = null;
            if (_dataContext.TryGetValue(key, out value))
                return value;

            TreeNode node = parent;
            while (node!=null)
            {
                value= node.GetDatat(key);
                if(value!=null) return value;
                node = node.parent;


            }
            return null;
        }

        public bool ClearDatat(string key)
        {

            if (_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;

            }

            TreeNode node = parent;
            while (node != null)
            {
                bool cleared = node.ClearDatat(key);
                if (cleared) return true;
                node = node.parent;


            }
            return false;
        }

    }

}
