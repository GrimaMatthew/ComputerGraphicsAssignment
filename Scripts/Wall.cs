using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    [SerializeField]
    private Vector3 size = Vector3.one;
    private List<Material> materialsList;

    [SerializeField]
    private int meshSize = 1;


    Vector3 t0;
    Vector3 t1;
    Vector3 t2;
    Vector3 t3;

    Vector3 b0;
    Vector3 b1;
    Vector3 b2;
    Vector3 b3;

    private void Start()
    {
        SquareMaker();
        meshCube();
        StartCoroutine(AddMat());
    }




    IEnumerator AddMat()
    {

        materialsList = new List<Material>();

        for (int i = 0; i <= 1; i++)
        {
            Material randMaterial = new Material(Shader.Find("Specular"));
            randMaterial.color = UnityEngine.Random.ColorHSV();
            materialsList.Add(randMaterial);

        }

        MeshRenderer meshRenderer = this.GetComponent<MeshRenderer>();
        meshRenderer.materials = materialsList.ToArray();

        yield return null;

    }





    private void SquareMaker()
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



        //Left-side Square
        meshBuilder.BuildTriangle(b1, t2, t1, 1);
        meshBuilder.BuildTriangle(b1, b2, t2, 1);

        //Front Square
        meshBuilder.BuildTriangle(b3, t0, t3, 1);
        meshBuilder.BuildTriangle(b3, b0, t0, 1);




        //set mesh filters mesh to the mesh generate from out Meshbuilder
        meshFilter.mesh = meshBuilder.CreateMesh();

    }
}