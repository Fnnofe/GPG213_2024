using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Diagnostics;
using System.Linq;
using System;

public class Pathfinding : MonoBehaviour
{
    GridMap grid;
    PathRequestManger requestManager;
     
    public void StartFindPath(Vector3 startPos,Vector3 targetPos)
    {
        StartCoroutine(FindPath(startPos, targetPos));

    }
    private void Awake()
    {
        requestManager = GetComponent<PathRequestManger>();
        grid=GetComponent<GridMap>();
    }
    IEnumerator FindPath(Vector3 startPos,Vector3 targetPos)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();


        Vector3[] waypoints = new Vector3[0];
        bool pathSucess = false;

        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        if (startNode.walkable && targetNode.walkable)
        {
            Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);

            //processing the nodes;
            while (openSet.Count > 0)
            {
                Node currentNode = openSet.RemoveFirst();
                //pick the lowest fCost node to check or equal to the curretn node.
                closedSet.Add(currentNode);

                //we found the end return the path.
                if (currentNode == targetNode)
                {
                    sw.Stop();
                    UnityEngine.Debug.Log("reached end" + sw.ElapsedMilliseconds + "ms");
                    pathSucess = true;
                    break;
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
                        else
                        {
                            openSet.UpdateItem(neighbour);
                        }



                    }

                }
            }

        }
        yield return null;
        if (pathSucess)
        {
         waypoints=RetracePath(startNode, targetNode);

        }
        requestManager.FinishedProcessingPath(waypoints, pathSucess);

    }

    Vector3[] RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode) 
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;
    }
    Vector3[] SimplifyPath(List<Node>path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld= Vector2.zero;
        for(int i=1; i < path.Count; i++)
        {
            //compare the parent node with the child node to see direction of movement. since all nodes evenly spaced out it will be the same value if they are moving in the same direction. 
            Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if (directionNew != directionOld)
            {
                waypoints.Add(path[i].worldPos);
            }
            directionOld = directionNew;
        }
        return waypoints.ToArray();

    }
    //
    int getDistance(Node nodeA, Node nodeB)
    {
        int distanceX=Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distanceY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (distanceX > distanceY) return 14 * distanceY + 10 * (distanceX - distanceY);

         return 14 * distanceX + 10 * (distanceY - distanceX);
    }






}
