using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
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
                new CheckIfInRange(transform),
                // new  myScripts.cs
                new Sequance(new List<TreeNode>
                {

                    // new  myScripts.cs
                    new EnemyMeleeAttack(),
                    new EnemyCastAttack(),
                    new EnemyRangeAttack(),
                    new CheckIfInRange(transform),
                }),
            }),
            new Sequance(new List<TreeNode>
            {
                new ChaseLogic(transform),
                new CheckIfFar(transform),


            }),
            new DespawnEnemy(),

        });


        return root;
    }
}
