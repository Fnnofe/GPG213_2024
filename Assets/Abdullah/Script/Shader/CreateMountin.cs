using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateMountin : MonoBehaviour
{
    //https://catlikecoding.com/unity/tutorials/procedural-grid/
    MeshRenderer meshRender;
    MeshFilter meshFilter;
    [SerializeField] Material material;
    public Vector2Int gridSize;
    Vector3[] myVerices;
    Vector2[] meshUVs;

    Vector3[] displacementVerts;
    Vector3[] originalVerts;
    public Texture2D displacmentTexture;
    public float displacementStrength;
    public bool updateVerts = false;
    public float mountainScale=3;

    void Start()
    {

        meshRender = gameObject.AddComponent<MeshRenderer>();
        meshFilter = gameObject.AddComponent<MeshFilter>();
        myVerices = new Vector3[(gridSize.x + 1) * (gridSize.y + 1)];
        meshUVs = new Vector2[myVerices.Length];
        
        GenerateMesh();


        
        // Add it to the Displacement map 
        displacementVerts = new Vector3[myVerices.Length];
        originalVerts = meshFilter.mesh.vertices;

        UpdateDisplacement();
        meshFilter.mesh.vertices = displacementVerts;
        meshFilter.mesh.RecalculateNormals();
        transform.localScale = Vector3.one* mountainScale; 

    }

    private void Update()
    {
        if (updateVerts == true) 
        {
        UpdateDisplacement();
        meshFilter.mesh.vertices = displacementVerts;
        meshFilter.mesh.RecalculateNormals();
        }
    }
    private void GenerateMesh()
    {
        Vector2 vertexPosition = Vector2.zero;
        Vector4[] tangents = new Vector4[myVerices.Length];
        Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);

        for (int y = 0, i = 0; y <= gridSize.y; ++y)
        {
            for (int x = 0; x <= gridSize.x; ++x, i++)
            {


                //place everything in the correct position case of 3X4.
                // 0  1  2  3
                // 4  5  6  7
                // 8  9  10 11
                vertexPosition.x = x;
                vertexPosition.y = y;
                myVerices[i] = new Vector3(vertexPosition.x, 0, vertexPosition.y);
                meshUVs[i] = new Vector2((float)x / gridSize.x, (float)y / gridSize.y);
                tangents[i] = tangent;

            }
        }

        meshFilter.mesh.vertices = myVerices;
        meshFilter.mesh.tangents = tangents;
        meshFilter.mesh.uv = meshUVs;
        int[] vertexSelector = new int[(myVerices.Length) * 6];

        //applay 
        for (int i = 0, vi = 0, y = 0; y < gridSize.y; y++, vi++)
        {
            for (int x = 0; x < gridSize.x; x++, i += 6, vi++)
            {
                //6 vertext for each square
                vertexSelector[i] = vi;
                vertexSelector[i + 3] = vertexSelector[i + 2] = vi + 1;
                vertexSelector[i + 4] = vertexSelector[i + 1] = vi + gridSize.x + 1;
                vertexSelector[i + 5] = vi + gridSize.x + 2;

                meshFilter.mesh.triangles = vertexSelector;
                meshRender.material = material;


                Debug.Log("-----" + vertexSelector.Length + "-----");

            }
        }
        meshFilter.mesh.RecalculateNormals();
    }

    private void UpdateDisplacement()
    {

        //displacement based on texture
        System.Array.Copy(originalVerts, displacementVerts, myVerices.Length);

        Color[] pixels = displacmentTexture.GetPixels();
        for (int i = 0; i < originalVerts.Length; i++)
        {
            // Sample displacement value from displacement map based on UV coordinates
            float displacement = pixels[GetUVIndex(i)].grayscale * displacementStrength;

            // Displace the vertex
            displacementVerts[i] = originalVerts[i] + transform.TransformDirection(Vector3.up) * displacement;

            Debug.Log("____displacmentTexture.width:  "+ displacmentTexture.width);
            Debug.Log("_____GetUVIndex(i):  "+ GetUVIndex(i));

        }
    }

    //helper
    int GetUVIndex(int vertexIndex)
        {
            // Ensure UVs are available and map them to vertex indices
            if (meshUVs != null && meshUVs.Length > 0)
            {
            // -1 is to keep it in bound
                return Mathf.FloorToInt(meshUVs[vertexIndex].x * (displacmentTexture.width - 1)) +Mathf.FloorToInt(meshUVs[vertexIndex].y * (displacmentTexture.height - 1)) * displacmentTexture.width;
            }
            else
            {
                // If UVs are not available, use vertex index as fallback
                return vertexIndex;
            }

        }

}
