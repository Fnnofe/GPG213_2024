using System.Collections.Generic;
using UnityEngine;

public class GridMap : MonoBehaviour
{

    public bool displayGrid = false;
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    Node[,] grid;

    float nodeDiaemeter;
    int gridSizeX, gridSizeY;

    void Awake()
    {
        nodeDiaemeter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiaemeter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiaemeter);
        CreateGrid();


    }


    public int MaxSize { get { return gridSizeX * gridSizeY; } }
    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiaemeter + nodeRadius) + Vector3.forward * (y * nodeDiaemeter + nodeRadius);
                bool wakable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                grid[x, y] = new Node(wakable, worldPoint, x, y);

            }

        }

    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y];
    }


    public List<Node> GetNeighbours(Node node)
    {


        List<Node> neighbours = new List<Node>();

        //search by 3X3 around the node reltively to it position.
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                //skip, it's the main node we are searching around.
                if (x == 0 && y == 0)
                {
                    continue;
                }
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;
                // check if it's inside the grid.
                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;

    }



    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 0.1f, gridWorldSize.y));



        if (grid != null && displayGrid == true)
        {
            foreach (Node node in grid)
            {


                Gizmos.color = Color.white;
                Gizmos.DrawCube(node.worldPos, Vector3.one * (nodeDiaemeter - .1f));
                if (node.walkable == false)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawCube(node.worldPos, Vector3.one * (nodeDiaemeter - .1f));


                }

            }
        }
    }
}
