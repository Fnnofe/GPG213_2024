using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree
{

    public abstract class TreeRoot : MonoBehaviour
    {
        private TreeNode _root = null;
        void Start()
        {
            _root = SetupTree();

        }

        // Update is called once per frame
       private void Update()
        {
            if(_root != null)
            {
                _root.Evaluate();
            }


        }
        protected abstract TreeNode SetupTree();


    }
}