using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : MonoBehaviour
{
    MeshRenderer meshRenderer;
    MeshFilter meshFilter;//holds the mesh.

   
    // Start is called before the first frame update
    //n+1 gives you the vertuies for one row. each rectangle has 6 indices.  
    void Start()
    {
        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer = gameObject.AddComponent<MeshRenderer>();

        meshFilter.mesh.vertices = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(1, 0, 0),
            new Vector3(1, 0, 1),
            new Vector3(0, 0, 1),
           

        };

        meshFilter.mesh.triangles = new int[]
       {
            0,
            1,
            2,
            2,
            3,
            0
           

       };


    }

    // Update is called once per frame
    void Update()
    {

    }
}