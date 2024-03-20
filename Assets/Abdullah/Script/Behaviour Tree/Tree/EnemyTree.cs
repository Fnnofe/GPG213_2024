using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using static PlasticPipe.PlasticProtocol.Messages.Serialization.ItemHandlerMessagesSerialization;
public class EnemyTree : TreeRoot
{
   // https://youtu.be/aR6wt5BlE-E?t=1311
    protected override TreeNode SetupTree()
    {
        //start the root select the first element.
        TreeNode root = new Selector(new List<TreeNode>
        { 
            new Sequance(new List<TreeNode>
            {
            new CheckIfInRange(),
                // new  myScripts.cs
                new Sequance(new List<TreeNode>
                {
                    // new  myScripts.cs
                    new EnemyMeleeAttack(),
                    new EnemyCastAttack(),
                    new EnemyRangeAttack(),
                    new CheckIfInRange(),
                }),
            }),

            new Sequance(new List<TreeNode>
            {
                new CheckIfFar(),
                new ChaseLogic(transform),


            }),
         new DespawnEnemy(),

        });


        return root;
    }
}
