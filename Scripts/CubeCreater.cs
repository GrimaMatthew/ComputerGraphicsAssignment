using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]



public class CubeCreater : MonoBehaviour
{

    [SerializeField]
    private Vector3 size = Vector3.one;
    private List<Material> materialsList;

    [SerializeField]
    private int meshSize = 6; // mesh size for the 6 differnet faces 

    //each of the points will make a square

    //top square
    Vector3 t0; 
    Vector3 t1;
    Vector3 t2;
    Vector3 t3;

    //bottom square

    Vector3 b0;
    Vector3 b1;
    Vector3 b2;
    Vector3 b3;

    private void Start()
    {
        cubeMaker();
        meshCube();
        StartCoroutine(AddMat());
    }

  


    IEnumerator AddMat() // adding material randomly 
    {
       
        materialsList = new List<Material>();

        for (int i = 0; i <= 6; i++)
        {
            Material randMaterial = new Material(Shader.Find("Specular"));
            randMaterial.color = UnityEngine.Random.ColorHSV();
            materialsList.Add(randMaterial);

        }

        MeshRenderer meshRenderer = this.GetComponent<MeshRenderer>();
        meshRenderer.materials = materialsList.ToArray();

        yield return null;

    }

    
  


    private void cubeMaker () 
    {
       
        //TopVertices
       t0 = new Vector3(size.x, size.y, -size.z);  //Top Left
       t1 = new Vector3(-size.x, size.y, -size.z); //Top Right
       t2 = new Vector3(-size.x, size.y, size.z);// Bottom right of the top square 
       t3 = new Vector3(size.x, size.y, size.z);//bottom left of the top square


        //Bottom Vertices(just a change in Y)

         b0 = new Vector3(size.x, -size.y, -size.z);
         b1 = new Vector3(-size.x, -size.y, -size.z); //bottom Right
         b2 = new Vector3(-size.x, -size.y, size.z);// Bottom right of the bottom square 
         b3 = new Vector3(size.x, -size.y, size.z);//bottom left of the bottom square

    }


    private void meshCube()
    {
        //initialise MeshFilter
        MeshFilter meshFilter = this.GetComponent<MeshFilter>();

        // intiialise MeshBuilder
        MeshBuilder meshBuilder = new MeshBuilder(meshSize);

        //Top Square always moving clockwise 
        //2Triangles
        meshBuilder.BuildTriangle(t0, t1, t2, 0); // The zero is the index of the sub mesh 
        meshBuilder.BuildTriangle(t0, t2, t3, 0);


        //Bottom Square
        meshBuilder.BuildTriangle(b2, b1, b0, 1);
        meshBuilder.BuildTriangle(b3, b2, b0, 1);

        //Back Square
        meshBuilder.BuildTriangle(b0, t1, t0, 2); // All are plus one from the previos moving in a clockwise direction
        meshBuilder.BuildTriangle(b0, b1, t1, 2);

        //Left-side Square
        meshBuilder.BuildTriangle(b1, t2, t1, 3);
        meshBuilder.BuildTriangle(b1, b2, t2, 3);

        //Right-side Square
        meshBuilder.BuildTriangle(b2, t3, t2, 4);
        meshBuilder.BuildTriangle(b2, b3, t3, 4);

        //Front Square
        meshBuilder.BuildTriangle(b3, t0, t3, 5);
        meshBuilder.BuildTriangle(b3, b0, t0, 5);


        //set mesh filters mesh to the mesh generate from out Meshbuilder
        meshFilter.mesh = meshBuilder.CreateMesh();

    }
}