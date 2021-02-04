using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class PlaneCreator : MonoBehaviour
{

    [SerializeField]
    private float cellSize = 10f;

    [SerializeField]
    private int width = 24;

    [SerializeField]
    private int height = 24;

    [SerializeField]
    private static int subMeshSize = 6;

    Vector3[,] points;

    MeshBuilder meshBuilder = new MeshBuilder(subMeshSize);

    // Start is called before the first frame update
    void Start()
    {

        MeshFilter meshFilter = this.GetComponent<MeshFilter>();

        Pointrender();
        Quadcreate();
        meshFilter.mesh = meshBuilder.CreateMesh();
        MaterialList();


    }


    private void MaterialList()
    {

        List<Material> materialsList = new List<Material>();


        for (int i = 0; i <= 6; i++)
        {
            Material randMaterial = new Material(Shader.Find("Specular"));
            randMaterial.color = UnityEngine.Random.ColorHSV();
            materialsList.Add(randMaterial);

        }

        MeshRenderer meshRenderer = this.GetComponent<MeshRenderer>();
        meshRenderer.materials = materialsList.ToArray();

    }


    private void Quadcreate()
    {
        int submesh = 0;

       
        //Create the quads 
        for (int x = 0; x < width - 1; x++)
        {
            for (int y = 0; y < height - 1; y++)
            {
                //+ 1 in any value to create missing block maze due to normal
                Vector3 br = points[x, y];
                Vector3 bl = points[x + 1, y]; 
                Vector3 tr = points[x, y + 1];
                Vector3 tl = points[x + 1, y + 1];

                // create 2 triangles that make up a quad
                meshBuilder.BuildTriangle(bl, tr, tl, submesh % subMeshSize);
                meshBuilder.BuildTriangle(bl, br, tr, submesh % subMeshSize);


            }

            submesh++;
        }
    }



    private void Pointrender()
    {
        //Create Points of our Plane

        points = new Vector3[width, height];
        for (int x = 0; x < width; x++) //going through rows
        {
            for (int y = 0; y < height; y++) // going through coloumns 
            {
                points[x, y] = new Vector3(
                    cellSize * x,
                    0, // Math.ping(x,10) | 0 for flat surface
                    cellSize * y);
            }
        }

    }

}
