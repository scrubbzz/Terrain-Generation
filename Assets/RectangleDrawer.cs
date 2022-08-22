using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectangleDrawer : MonoBehaviour
{
    MeshRenderer meshRenderer;
    MeshFilter meshFilter;

    [SerializeField] int rectangleCountX;//Rectangle count X and Z are the only ones we will have to change manually.
    [SerializeField] int rectangleCountZ;
    [SerializeField] float elevationFactor;//By how much we want to raise each vertex on the y axis.
    [SerializeField] Texture2D heightMap;

    int totalRectangles = 0;//Total rectangles on the grid.
    
    int totalVerticesX = 0;
    int totalVerticesZ = 0;
    [SerializeField] int totalVertexRows = 0;

    const int totalIndicesPerRectangle = 6;//This value will always be 6, it is how many times we need to connect vertices to make one rectangle.
    int totalIndices = 0;

    //private int rectangleIteration;

    // Start is called before the first frame update
    void Start()
    {
        totalRectangles = rectangleCountX * rectangleCountZ;

        totalVerticesX = rectangleCountX + 1;
        totalVerticesZ = rectangleCountZ + 1;
        totalVertexRows = totalVerticesX * totalVerticesZ;

        totalIndices = totalIndicesPerRectangle * totalRectangles;

        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer = gameObject.AddComponent<MeshRenderer>();

        CreateVertexArray();
        CreateIndexArray();

    }

    void Update()
    {

    }

    public void CreateVertexArray()
    {
        Vector3[] vertices = new Vector3[totalVertexRows];
        float ratioX = (float)heightMap.width / (float)totalVerticesX;//The ratio of the number of pixels of the image on x-axis to totalVerticesX.
        float ratioZ = (float)heightMap.height / (float)totalVerticesZ;//The ratio of the number of pixels of the image on z-axis to totalVerticesZ.
        for (int i = 0, z = 0; z < totalVerticesZ; z++)
        {
            for (int x = 0; x < totalVerticesX; x++)
            {
                Color color = heightMap.GetPixel((int)(x * ratioX), (int)(z * ratioZ));//Pixel determines on the image we have used as the heightMap. Multiplying by ratio so its relative value is adjusted accordingly.
                vertices[i] = new Vector3(x, color.r * elevationFactor, -z);//The y value for each vertex is set according to coordinate value y of the "color" variable.
                i++;
            }
        }
        meshFilter.mesh.vertices = vertices;
    }

    public void CreateIndexArray()
    {
        int[] indices = new int[totalIndices];
        int currentRectangle = 0;//Represents the current rectangle we want to draw.
        int currentVertex = 0;//Represents the total number of vertices on that particular row.

        for (int z = 0; z < rectangleCountZ; z++)
        {
            for (int x = 0; x < rectangleCountX; x++)
            {
                indices[currentRectangle + 0] = currentVertex;
                indices[currentRectangle + 1] = currentVertex + 1;
                indices[currentRectangle + 2] = currentVertex + totalVerticesX + 1;//If you simply add totalVerticesX, then you move 1 up to the above row, then adding one moves one to the right...
                indices[currentRectangle + 3] = currentVertex + totalVerticesX + 1;
                indices[currentRectangle + 4] = currentVertex + totalVerticesX;
                indices[currentRectangle + 5] = currentVertex;

                currentVertex++;
                currentRectangle += 6;//Moving on to the next 'set' of indices we want to use to draw the next rectangle.
            }
            currentVertex++;//Moves one row up then the above block of code is repeated until we have drawn all the rows of rectangles we want (rectangleCountZ).
        }

        meshFilter.mesh.triangles = indices;
    }

   /* private void OnDrawGizmos()
    {
        if (meshFilter.mesh.vertices == null)
        {
            return;
        }

        for (int i = 0; i < meshFilter.mesh.vertices.Length; i++)
        {
            Gizmos.DrawSphere(meshFilter.mesh.vertices[i], 0.1f);
        }
    }*/
}
