using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Pathfinding_old : MonoBehaviour
{
    GridMap grid;
    public Transform enemy, targetPlayer;

    private void Awake()
    {
        grid = GetComponent<GridMap>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FindPath(enemy.position, targetPlayer.position);
        }
    }
    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);
        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        //processing the nodes;
        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            //pick the lowest fCost node to check or equal to the curretn node.
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];

                }

            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            //we found the end return the path.
            if (currentNode == targetNode)
            {
                sw.Stop();
                UnityEngine.Debug.Log("reached end" + sw.ElapsedMilliseconds + "ms");

                RetracePath(startNode, targetNode);
                return;
            }
            foreach (Node neighbour in grid.GetNeighbours(currentNode))
            {
                //check if surrounding nodes are closed or unwalkable.
                if (!neighbour.walkable || closedSet.Contains(neighbour)) { continue; }

                //calculate 
                int newMovementCostToNeighbour = currentNode.gCost + getDistance(currentNode, neighbour);
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {

                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = getDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }



                }

            }
        }
    }

    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;
        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        // grid.path = path;

    }

    //
    int getDistance(Node nodeA, Node nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distanceY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (distanceX > distanceY) return 14 * distanceY + 10 * (distanceX - distanceY);

        return 14 * distanceX + 10 * (distanceY - distanceX);
    }




}
